// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : UserStoryEstimation.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities;
    using Mapping;

    public sealed class UserStoryEstimation : IMapFrom<UserStory> {
        public int Id { get; }

        public string? Title { get; }

        public ICollection<EstimationModel> Estimations { get; }

        public UserStoryEstimation(int id, string? title, IEnumerable<EstimationModel> estimations) {
            List<EstimationModel> allEstimations = estimations.ToList();

            this.Id = id;
            this.Title = title;
            this.Estimations = allEstimations.OrderByDescending(x => allEstimations.Count(e => e.Symbol.Id == x.Symbol.Id)).ToList();
        }

        // This constructor exists for the automapping
        public UserStoryEstimation() {
            this.Id = 0;
            this.Title = null;
            this.Estimations = Array.Empty<EstimationModel>();
        }
    }
}
