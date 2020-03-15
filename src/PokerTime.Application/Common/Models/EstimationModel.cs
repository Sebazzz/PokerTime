// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : EstimationModel.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Models {
    using System;
    using AutoMapper;
    using Domain.Entities;
    using Mapping;

#nullable disable

    public sealed class EstimationModel : IMapFrom<Estimation> {
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public ColorModel ParticipantColor { get; set; }

        public SymbolModel Symbol { get; set; }

        public void Mapping(Profile profile) {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            profile.CreateMap<Estimation, EstimationModel>();
        }
    }
}
