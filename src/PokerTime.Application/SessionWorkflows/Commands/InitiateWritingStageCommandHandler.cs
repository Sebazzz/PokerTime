// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateWritingStageCommandHandler.cs
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
    using PokerTime.Common;

    public sealed class InitiateWritingStageCommandHandler : AbstractStageCommandHandler<InitiateWritingStageCommand> {
        private readonly ISystemClock _systemClock;

        public InitiateWritingStageCommandHandler(IPokerTimeDbContext pokerTimeDbContext, ISessionStatusUpdateDispatcher retrospectiveStatusUpdateDispatcher, ISystemClock systemClock) : base(pokerTimeDbContext, retrospectiveStatusUpdateDispatcher) {
            this._systemClock = systemClock;
        }

        protected override async Task<Unit> HandleCore(InitiateWritingStageCommand request, Session session, CancellationToken cancellationToken) {
            if (session == null) throw new ArgumentNullException(nameof(session));

            session.CurrentStage = SessionStage.Writing;

            await this.DbContext.SaveChangesAsync(cancellationToken);

            await this.DispatchUpdate(session, cancellationToken);

            return Unit.Value;
        }
    }
}
