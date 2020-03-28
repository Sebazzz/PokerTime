// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusMapper.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetSessionStatus {
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Abstractions;
    using Common.Models;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface ISessionStatusMapper {
        Task<SessionStatus> GetSessionStatus(Session session, CancellationToken cancellationToken);
    }

    public sealed class SessionStatusMapper : ISessionStatusMapper {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IMapper _mapper;

        public SessionStatusMapper(IPokerTimeDbContext pokerTimeDbContext, IMapper mapper) {
            this._mapper = mapper;
            this._pokerTimeDbContext = pokerTimeDbContext;
        }

        public async Task<SessionStatus> GetSessionStatus(Session session, CancellationToken cancellationToken) {
            if (session == null) throw new ArgumentNullException(nameof(session));

            UserStory? currentUserStory = await this._pokerTimeDbContext.UserStories.Where(x => x.SessionId == session.Id).
                OrderByDescending(x => x.Id).
                FirstOrDefaultAsync(cancellationToken);

            UserStoryModel? currentUserStoryModel = currentUserStory != null ? this._mapper.Map<UserStoryModel>(currentUserStory) : null;
            var sessionStatus = new SessionStatus(session.UrlId.StringId, session.Title, session.CurrentStage, session.SymbolSetId, currentUserStoryModel);

            return sessionStatus;
        }
    }
}
