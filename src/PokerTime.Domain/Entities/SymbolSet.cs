// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SymbolSet.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Entities {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a set of symbols that can be chosen within an estimation session
    /// </summary>
    public sealed class SymbolSet {
        private ICollection<Symbol>? _symbols;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Symbol> Symbols => this._symbols ??= new Collection<Symbol>();
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}
