// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSymbolsQueryResponse.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Symbols.Queries {
    using System.Collections.Generic;
    using Common.Models;

    public sealed class GetSymbolsQueryResponse {
        public ICollection<SymbolModel> Symbols { get; }

        internal GetSymbolsQueryResponse(ICollection<SymbolModel> symbols) {
            this.Symbols = symbols;
        }
    }
}
