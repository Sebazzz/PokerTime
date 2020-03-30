// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsOverviewQueryResponse.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;

    public sealed class GetEstimationsOverviewQueryResponse {
        public ICollection<UserStoryEstimation> UserStoryEstimations { get; }

        public GetEstimationsOverviewQueryResponse(IEnumerable<UserStoryEstimation> userStoryEstimations) {
            this.UserStoryEstimations = userStoryEstimations.ToList();
        }
    }
}
