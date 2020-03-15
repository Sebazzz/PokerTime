// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
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
    using PokerTime.Common;

    public sealed class InitiateDiscussionStageCommandHandler : AbstractStageCommandHandler<InitiateDiscussionStageCommand> {
        private readonly ISystemClock _systemClock;

        public InitiateDiscussionStageCommandHandler(IPokerTimeDbContext pokerTimeDbContext, ISessionStatusUpdateDispatcher retrospectiveStatusUpdateDispatcher, ISystemClock systemClock) : base(pokerTimeDbContext, retrospectiveStatusUpdateDispatcher) {
            this._systemClock = systemClock;
        }

        protected override async Task<Unit> HandleCore(InitiateDiscussionStageCommand request, Session session, CancellationToken cancellationToken) {
            if (session == null) throw new ArgumentNullException(nameof(session));

            session.CurrentStage = SessionStage.Discussion;

            var userStory = new UserStory
            {
                Session = session,
                Title = String.IsNullOrEmpty(request.UserStoryTitle) ? null : request.UserStoryTitle
            };
            session.CurrentUserStory = userStory;

            this.DbContext.UserStories.Add(userStory);

            await this.DbContext.SaveChangesAsync(cancellationToken);

            await this.DispatchUpdate(session, cancellationToken);

            return Unit.Value;
        }
    }
}
