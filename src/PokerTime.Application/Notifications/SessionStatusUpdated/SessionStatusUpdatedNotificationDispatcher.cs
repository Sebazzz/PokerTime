// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusUpdatedNotificationDispatcher.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.SessionStatusUpdated {
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public sealed class SessionStatusUpdatedNotificationDispatcher : NotificationDispatcher<
        SessionStatusUpdatedNotification, ISessionStatusUpdatedSubscriber> {
        public SessionStatusUpdatedNotificationDispatcher(ILogger<NotificationDispatcher<SessionStatusUpdatedNotification, ISessionStatusUpdatedSubscriber>> logger) : base(logger)
        {
        }

        protected override Task DispatchCore(
            ISessionStatusUpdatedSubscriber subscriber,
            SessionStatusUpdatedNotification notification
        ) => subscriber.OnSessionStatusUpdated(notification.SessionStatus);
    }
}
