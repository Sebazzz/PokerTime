// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusUpdatedNotification.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.SessionStatusUpdated {
    using MediatR;
    using Sessions.Queries.GetSessionStatus;

    public sealed class SessionStatusUpdatedNotification : INotification {
        public SessionStatus SessionStatus { get; }

        public SessionStatusUpdatedNotification(SessionStatus sessionStatus) {
            this.SessionStatus = sessionStatus;
        }
    }
}
