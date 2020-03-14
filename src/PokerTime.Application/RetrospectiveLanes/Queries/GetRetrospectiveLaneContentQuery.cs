// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetRetrospectiveLaneContent.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveLanes.Queries {
    using MediatR;

    public sealed class GetRetrospectiveLaneContentQuery : IRequest<RetrospectiveLaneContent> {
        public string RetroId { get; }
        public int LaneId { get; }

        public GetRetrospectiveLaneContentQuery(string retroId, int laneId) {
            this.RetroId = retroId;
            this.LaneId = laneId;
        }
    }
}
