// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveJoinedNotificationDispatcher.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.RetrospectiveJoined {
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Retrospectives.Queries.GetParticipantsInfo;

    public sealed class RetrospectiveJoinedNotificationDispatcher : NotificationDispatcher<RetrospectiveJoinedNotification, IRetrospectiveJoinedSubscriber> {
        public RetrospectiveJoinedNotificationDispatcher(ILogger<NotificationDispatcher<RetrospectiveJoinedNotification, IRetrospectiveJoinedSubscriber>> logger) : base(logger)
        {
        }

        protected override Task DispatchCore(IRetrospectiveJoinedSubscriber subscriber, RetrospectiveJoinedNotification notification) => subscriber.OnParticipantJoinedRetrospective(new RetrospectiveEvent<ParticipantInfo>(notification.SessionId, notification.ParticipantInfo));
    }
}
