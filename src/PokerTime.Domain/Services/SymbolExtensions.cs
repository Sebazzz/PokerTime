// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SymbolExtensions.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Services {
    using System;
    using System.Globalization;
    using Entities;

    public static class SymbolExtensions {
        public static string GetValueAsString(this Symbol symbol) {
            if (symbol == null) throw new ArgumentNullException(nameof(symbol));

            switch (symbol.Type) {
                case SymbolType.Number:
                    return symbol.ValueAsNumber.ToString(CultureInfo.InvariantCulture);
                case SymbolType.Infinite:
                    return "∞";
                case SymbolType.Break:
                    return "☕";
                default:
                    throw new ArgumentOutOfRangeException($"Unknown symbol type: {symbol.Type}");
            }
        }
    }
}
