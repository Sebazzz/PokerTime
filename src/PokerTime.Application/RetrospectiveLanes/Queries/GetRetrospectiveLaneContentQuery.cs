// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetRetrospectiveLaneContent.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveLanes.Queries {
    using MediatR;

    public sealed class GetRetrospectiveLaneContentQuery : IRequest<RetrospectiveLaneContent> {
        public string SessionId { get; }
        public int LaneId { get; }

        public GetRetrospectiveLaneContentQuery(string sessionId, int laneId) {
            this.SessionId = sessionId;
            this.LaneId = laneId;
        }
    }
}
