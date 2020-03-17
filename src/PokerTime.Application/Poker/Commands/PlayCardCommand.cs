// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PlayCardCommand.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Poker.Commands {
    using MediatR;

    public sealed class PlayCardCommand : IRequest {
        public string SessionId { get; }

        public int UserStoryId { get; }

        public int SymbolId { get; }

        public PlayCardCommand(string sessionId, int userStoryId, int symbolId) {
            this.SessionId = sessionId;
            this.UserStoryId = userStoryId;
            this.SymbolId = symbolId;
        }
    }
}
