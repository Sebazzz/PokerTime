// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusMapper.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetSessionStatus {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Abstractions;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface ISessionStatusMapper {
        Task<SessionStatus> GetSessionStatus(Retrospective retrospective, CancellationToken cancellationToken);
    }

    public sealed class SessionStatusMapper : ISessionStatusMapper {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IMapper _mapper;

        public SessionStatusMapper(IPokerTimeDbContext pokerTimeDbContext, IMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public Task<SessionStatus> GetSessionStatus(Retrospective retrospective, CancellationToken cancellationToken) {
            if (retrospective == null) throw new ArgumentNullException(nameof(retrospective));

            var workflowStatus = RetrospectiveWorkflowStatus.FromDomainWorkflowData(retrospective.WorkflowData);
            var retrospectiveStatus = new SessionStatus(retrospective.UrlId.StringId, retrospective.Title, retrospective.CurrentStage, workflowStatus, retrospective.Options.MaximumNumberOfVotes);

            return Task.FromResult(retrospectiveStatus);
        }
    }
}
