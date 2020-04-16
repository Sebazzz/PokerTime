// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : EstimationGivenNotificationDispatcher.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.EstimationGiven {
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public sealed class EstimationGivenNotificationDispatcher : NotificationDispatcher<EstimationGivenNotification, IEstimationGivenSubscriber> {
        public EstimationGivenNotificationDispatcher(ILogger<NotificationDispatcher<EstimationGivenNotification, IEstimationGivenSubscriber>> logger) : base(logger) {
        }

        protected override Task DispatchCore(
            IEstimationGivenSubscriber subscriber,
            EstimationGivenNotification notification
        ) => subscriber.OnEstimationGiven(notification);
    }
}
