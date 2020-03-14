// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveEvent.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications {
    public sealed class RetrospectiveEvent<T> {
        public string SessionId { get; }

        public T Argument { get; }

        public RetrospectiveEvent(string sessionId, T argument) {
            this.SessionId = sessionId;
            this.Argument = argument;
        }
    }
}
