// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateEstimationStageCommandHandler.cs
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

    public sealed class InitiateEstimationStageCommandHandler : AbstractStageCommandHandler<InitiateEstimationStageCommand> {
        public InitiateEstimationStageCommandHandler(IPokerTimeDbContextFactory pokerTimeDbContext, ISessionStatusUpdateDispatcher sessionStatusUpdateDispatcher) : base(pokerTimeDbContext, sessionStatusUpdateDispatcher) {
        }

        protected override async Task<Unit> HandleCore(InitiateEstimationStageCommand request, Session session, CancellationToken cancellationToken) {
            if (session == null) throw new ArgumentNullException(nameof(session));

            session.CurrentStage = SessionStage.Estimation;

            await this.DbContext.SaveChangesAsync(cancellationToken);

            await this.DispatchUpdate(session, cancellationToken);

            return Unit.Value;
        }
    }
}
