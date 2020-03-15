// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusUpdateDispatcher.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SessionWorkflows.Common {
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using MediatR;
    using Notifications.SessionStatusUpdated;
    using Sessions.Queries.GetSessionStatus;

    public interface ISessionStatusUpdateDispatcher {
        Task DispatchUpdate(Session session, CancellationToken cancellationToken);
    }

    public sealed class SessionStatusUpdateDispatcher : ISessionStatusUpdateDispatcher {
        private readonly ISessionStatusMapper _sessionStatusMapper;
        private readonly IMediator _mediator;

        public SessionStatusUpdateDispatcher(ISessionStatusMapper sessionStatusMapper, IMediator mediator) {
            this._sessionStatusMapper = sessionStatusMapper;
            this._mediator = mediator;
        }

        public async Task DispatchUpdate(Session session, CancellationToken cancellationToken) {
            SessionStatus sessionStatus = await this._sessionStatusMapper.GetSessionStatus(session, cancellationToken);

            await this._mediator.Publish(new SessionStatusUpdatedNotification(sessionStatus), cancellationToken);
        }
    }
}
