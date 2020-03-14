// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateDiscussionStageCommandHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SessionWorkflows.Commands {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Common;
    using Domain.Entities;
    using MediatR;

    public sealed class InitiateFinishStageCommandHandler : AbstractStageCommandHandler<InitiateFinishStageCommand> {
        public InitiateFinishStageCommandHandler(IPokerTimeDbContextFactory pokerTimeDbContext, ISessionStatusUpdateDispatcher retrospectiveStatusUpdateDispatcher) : base(pokerTimeDbContext, retrospectiveStatusUpdateDispatcher) {
        }

        protected override async Task<Unit> HandleCore(InitiateFinishStageCommand request, Retrospective retrospective, CancellationToken cancellationToken) {
            if (retrospective == null) throw new ArgumentNullException(nameof(retrospective));

            retrospective.CurrentStage = SessionStage.Finished;

            await this.DbContext.SaveChangesAsync(cancellationToken);

            await this.DispatchUpdate(retrospective, cancellationToken);

            return Unit.Value;
        }
    }
}
