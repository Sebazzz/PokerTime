// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSymbolSetsQueryResponse.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SymbolSets.Queries {
    using System;
    using Common.Models;

    public sealed class GetSymbolSetsQueryResponse {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "This is a unique array, not shared")]
        public SymbolSetModel[] SymbolSets { get; }

        public GetSymbolSetsQueryResponse(SymbolSetModel[] symbolSets) {
            this.SymbolSets = symbolSets ?? throw new ArgumentNullException(nameof(symbolSets));
        }
    }
}
