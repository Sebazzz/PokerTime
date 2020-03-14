// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveEvent.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications {
    public sealed class RetrospectiveEvent<T> {
        public string RetroId { get; }

        public T Argument { get; }

        public RetrospectiveEvent(string retroId, T argument) {
            this.RetroId = retroId;
            this.Argument = argument;
        }
    }
}
