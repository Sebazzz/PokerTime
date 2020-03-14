// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveStatusMapper.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetRetrospectiveStatus {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Abstractions;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IRetrospectiveStatusMapper {
        Task<RetrospectiveStatus> GetRetrospectiveStatus(Retrospective retrospective, CancellationToken cancellationToken);
    }

    public sealed class RetrospectiveStatusMapper : IRetrospectiveStatusMapper {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IMapper _mapper;

        public RetrospectiveStatusMapper(IPokerTimeDbContext pokerTimeDbContext, IMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public async Task<RetrospectiveStatus> GetRetrospectiveStatus(Retrospective retrospective, CancellationToken cancellationToken) {
            if (retrospective == null) throw new ArgumentNullException(nameof(retrospective));

            var workflowStatus = RetrospectiveWorkflowStatus.FromDomainWorkflowData(retrospective.WorkflowData);
            var retrospectiveStatus = new RetrospectiveStatus(retrospective.UrlId.StringId, retrospective.Title, retrospective.CurrentStage, workflowStatus, retrospective.Options.MaximumNumberOfVotes);

            retrospectiveStatus.Lanes.AddRange(await this._pokerTimeDbContext.NoteLanes.AsNoTracking().ProjectTo<RetrospectiveLane>(this._mapper.ConfigurationProvider).ToListAsync(cancellationToken));

            return retrospectiveStatus;
        }
    }
}
