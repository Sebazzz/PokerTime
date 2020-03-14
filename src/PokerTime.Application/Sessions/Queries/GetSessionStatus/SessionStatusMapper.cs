// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusMapper.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetSessionStatus {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Abstractions;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface ISessionStatusMapper {
        Task<SessionStatus> GetSessionStatus(Session session, CancellationToken cancellationToken);
    }

    public sealed class SessionStatusMapper : ISessionStatusMapper {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IMapper _mapper;

        public SessionStatusMapper(IPokerTimeDbContext pokerTimeDbContext, IMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public Task<SessionStatus> GetSessionStatus(Session session, CancellationToken cancellationToken) {
            if (session == null) throw new ArgumentNullException(nameof(session));

            var retrospectiveStatus = new SessionStatus(session.UrlId.StringId, session.Title, session.CurrentStage);

            return Task.FromResult(retrospectiveStatus);
        }
    }
}
