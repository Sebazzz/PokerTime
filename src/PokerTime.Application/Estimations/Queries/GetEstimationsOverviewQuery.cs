// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsOverviewQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using Common.Models;

    public sealed class GetEstimationsOverviewQuery {
        public string SessionId { get; }

        public GetEstimationsOverviewQuery(string sessionId) {
            this.SessionId = sessionId;
        }
    }

    public sealed class GetEstimationsOverviewQueryResponse {

    }

    public sealed class UserStoryEstimation {
        public UserStoryModel
    }
}
