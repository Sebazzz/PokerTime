// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateDicussionStageCommandHandler.cs
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

    public sealed class InitiateDicussionStageCommandHandler : AbstractStageCommandHandler<InitiateDiscussionStageCommand> {
        private readonly ISystemClock _systemClock;

        public InitiateDicussionStageCommandHandler(IPokerTimeDbContext pokerTimeDbContext, ISessionStatusUpdateDispatcher retrospectiveStatusUpdateDispatcher, ISystemClock systemClock) : base(pokerTimeDbContext, retrospectiveStatusUpdateDispatcher) {
            this._systemClock = systemClock;
        }

        protected override async Task<Unit> HandleCore(InitiateDiscussionStageCommand request, Session session, CancellationToken cancellationToken) {
            if (session == null) throw new ArgumentNullException(nameof(session));

            session.CurrentStage = SessionStage.Discussion;

            await this.DbContext.SaveChangesAsync(cancellationToken);

            await this.DispatchUpdate(session, cancellationToken);

            return Unit.Value;
        }
    }
}
