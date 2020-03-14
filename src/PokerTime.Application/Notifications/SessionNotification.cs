// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionNotification.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications {
    using MediatR;

    public abstract class SessionNotification : INotification {
        protected SessionNotification(string sessionId) {
            this.SessionId = sessionId;
        }

        public string SessionId { get; }
    }
}
