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

    public sealed class InitiateDiscussionStageCommandHandler : AbstractStageCommandHandler<InitiateDiscussionStageCommand> {
        public InitiateDiscussionStageCommandHandler(IPokerTimeDbContext pokerTimeDbContext, ISessionStatusUpdateDispatcher sessionStatusUpdateDispatcher) : base(pokerTimeDbContext, sessionStatusUpdateDispatcher) {
        }

        protected override async Task<Unit> HandleCore(InitiateDiscussionStageCommand request, Session session, CancellationToken cancellationToken) {
            if (session == null) throw new ArgumentNullException(nameof(session));

            session.CurrentStage = request.IsReestimation ? SessionStage.Estimation : SessionStage.Discussion;

            var userStory = new UserStory {
                Session = session,
                Title = String.IsNullOrEmpty(request.UserStoryTitle) ? null : request.UserStoryTitle
            };

            this.DbContext.UserStories.Add(userStory);

            await this.DbContext.SaveChangesAsync(cancellationToken);

            await this.DispatchUpdate(session, cancellationToken);

            return Unit.Value;
        }
    }
}
