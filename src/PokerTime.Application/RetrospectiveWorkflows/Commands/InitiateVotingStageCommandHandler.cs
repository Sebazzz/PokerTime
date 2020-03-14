// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateVotingStageCommandHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveWorkflows.Commands {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Common;
    using Domain.Entities;
    using MediatR;
    using PokerTime.Common;

    public sealed class InitiateVotingStageCommandHandler : AbstractStageCommandHandler<InitiateVotingStageCommand> {
        private readonly ISystemClock _systemClock;

        public InitiateVotingStageCommandHandler(IPokerTimeDbContext pokerTimeDbContext, IRetrospectiveStatusUpdateDispatcher retrospectiveStatusUpdateDispatcher, ISystemClock systemClock) : base(pokerTimeDbContext, retrospectiveStatusUpdateDispatcher) {
            this._systemClock = systemClock;
        }

        protected override async Task<Unit> HandleCore(InitiateVotingStageCommand request, Retrospective retrospective, CancellationToken cancellationToken) {
            if (retrospective == null) throw new ArgumentNullException(nameof(retrospective));

            retrospective.CurrentStage = RetrospectiveStage.Voting;
            retrospective.WorkflowData.CurrentWorkflowInitiationTimestamp = this._systemClock.CurrentTimeOffset;
            retrospective.WorkflowData.CurrentWorkflowTimeLimitInMinutes = request.TimeInMinutes;

            retrospective.Options.MaximumNumberOfVotes = request.VotesPerGroup;

            await this.DbContext.SaveChangesAsync(cancellationToken);

            await this.DispatchUpdate(retrospective, cancellationToken);

            return Unit.Value;
        }
    }
}
