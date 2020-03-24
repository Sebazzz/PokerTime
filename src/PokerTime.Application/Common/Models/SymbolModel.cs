// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SymbolModel.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Models {
    using System;
    using AutoMapper;
    using Domain.Entities;
    using Mapping;

    public sealed class SymbolModel : IMapFrom<Symbol> {
        public int Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string AsString { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public int Order { get; set; }

        public void Mapping(Profile profile) {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            profile.CreateMap<Symbol, SymbolModel>().
                ForMember(x => x.AsString,
                    m => m.MapFrom(x => x.ValueAsString));
        }
    }
}
