// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CurrentUserStoryModel.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Models {
    using System;
    using AutoMapper;
    using Domain.Entities;
    using Mapping;

    public sealed class UserStoryModel : IMapFrom<UserStory> {
        public int Id { get; set; }
        public string? Title { get; set; }

        public void Mapping(Profile profile) {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            profile.CreateMap<UserStory, UserStoryModel>();
        }
    }
}
