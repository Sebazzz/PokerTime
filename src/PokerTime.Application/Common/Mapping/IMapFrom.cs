// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : Interface1.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Mapping {
    using System;
    using AutoMapper;

    public interface IMapFrom<T> {
        void Mapping(Profile profile) {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            profile.CreateMap(typeof(T), this.GetType());
        }
    }
}
