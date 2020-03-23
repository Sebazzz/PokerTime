// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SymbolSetModel.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Models {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Domain.Entities;
    using Mapping;

    public sealed class SymbolSetModel : IMapFrom<SymbolSet> {
        public SymbolSetModel(int id, string name, IEnumerable<SymbolModel> symbols) {
            this.Id = id;
            this.Name = name;
            this.Symbols = new ReadOnlyCollection<SymbolModel>(symbols.OrderBy(keySelector: x => x.Order).ToList());
        }

        public SymbolSetModel() {
            this.Name = String.Empty;
            this.Symbols = new ReadOnlyCollection<SymbolModel>(new List<SymbolModel>(0));
        }

        public int Id { get; }

        public string Name { get; }

        public ReadOnlyCollection<SymbolModel> Symbols { get; }
    }
}
