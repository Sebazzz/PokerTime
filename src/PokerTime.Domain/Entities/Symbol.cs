// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : Symbol.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Entities {
    /// <summary>
    /// Represents a symbol that can be chosen in an estimation
    /// </summary>
    public class Symbol {
        public int Id { get; set; }

        /// <summary>
        /// In case of <see cref="SymbolType.Number"/>, the numerical value
        /// Else, a relative value, which can be used to compare the cards
        /// </summary>
        public int? ValueAsNumber { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string ValueAsString { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public SymbolType Type { get; set; }

        public int Order { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public SymbolSet SymbolSet { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public int SymbolSetId { get; set; }
    }

    public enum SymbolType {
        Number,

        Infinite,

        Break,

        Characters
    }
}
