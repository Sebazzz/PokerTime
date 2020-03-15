// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using MediatR;

    public sealed class GetEstimationsQuery : IRequest<GetEstimationsQueryResponse> {
        public string SessionId { get; }

        public int UserStoryId { get; }

        public GetEstimationsQuery(string sessionId, int userStoryId) {
            this.SessionId = sessionId;
            this.UserStoryId = userStoryId;
        }
    }
}
