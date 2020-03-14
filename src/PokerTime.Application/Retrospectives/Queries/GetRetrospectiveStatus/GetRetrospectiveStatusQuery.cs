// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetRetrospectiveStatusQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetRetrospectiveStatus {
    using MediatR;

    public sealed class GetRetrospectiveStatusQuery : IRequest<RetrospectiveStatus> {
        public string SessionId { get; }

        public GetRetrospectiveStatusQuery(string sessionId) {
            this.SessionId = sessionId;
        }
    }
}
