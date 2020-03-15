// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsQueryResponse.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using System.Collections.Generic;
    using Common.Models;

    public sealed class GetEstimationsQueryResponse {
        public ICollection<EstimationModel> Estimations { get; }

        public GetEstimationsQueryResponse(ICollection<EstimationModel> estimations) {
            this.Estimations = estimations;
        }
    }
}
