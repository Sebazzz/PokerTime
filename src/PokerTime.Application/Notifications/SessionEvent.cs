// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionEvent.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications {
    public sealed class SessionEvent<T> {
        public string SessionId { get; }

        public T Argument { get; }

        public SessionEvent(string sessionId, T argument) {
            this.SessionId = sessionId;
            this.Argument = argument;
        }
    }
}
