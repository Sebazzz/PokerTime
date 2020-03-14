// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusUpdateDispatcher.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveWorkflows.Common {
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using MediatR;
    using Notifications.SessionStatusUpdated;
    using Sessions.Queries.GetSessionStatus;

    public interface ISessionStatusUpdateDispatcher {
        Task DispatchUpdate(Retrospective retrospective, CancellationToken cancellationToken);
    }

    public sealed class SessionStatusUpdateDispatcher : ISessionStatusUpdateDispatcher {
        private readonly ISessionStatusMapper _retrospectiveStatusMapper;
        private readonly IMediator _mediator;

        public SessionStatusUpdateDispatcher(ISessionStatusMapper retrospectiveStatusMapper, IMediator mediator) {
            this._retrospectiveStatusMapper = retrospectiveStatusMapper;
            this._mediator = mediator;
        }

        public async Task DispatchUpdate(Retrospective retrospective, CancellationToken cancellationToken) {
            SessionStatus SessionStatus = await this._retrospectiveStatusMapper.GetSessionStatus(retrospective, cancellationToken);

            await this._mediator.Publish(new SessionStatusUpdatedNotification(SessionStatus), cancellationToken);
        }
    }
}
