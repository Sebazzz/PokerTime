// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PlayCardCommand.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Poker.Commands {
    using Common.Models;
    using MediatR;

    public sealed class PlayCardCommand : IRequest {
        public int UserStoryId { get; }

        public SymbolModel Symbol { get; }

        public PlayCardCommand(int userStoryId, SymbolModel symbol) {
            this.UserStoryId = userStoryId;
            this.Symbol = symbol;
        }
    }
}
