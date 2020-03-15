// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : Symbol.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Entities {
    public class Symbol {
        public int Id { get; set; }

        public int ValueAsNumber { get; set; }

        public SymbolType Type { get; set; }

        public int Order { get; set; }
    }

    public enum SymbolType {
        Number,

        Infinite,

        Break,

        Unknown
    }
}
