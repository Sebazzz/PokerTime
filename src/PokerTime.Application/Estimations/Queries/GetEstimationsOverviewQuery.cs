// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsOverviewQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using MediatR;

    public sealed class GetEstimationsOverviewQuery : IRequest<GetEstimationsOverviewQueryResponse> {
        public string SessionId { get; }

        public GetEstimationsOverviewQuery(string sessionId) {
            this.SessionId = sessionId;
        }
    }
}
