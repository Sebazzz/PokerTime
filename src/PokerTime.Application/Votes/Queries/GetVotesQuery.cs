// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetVotesQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Votes.Queries {
    using MediatR;

    public sealed class GetVotesQuery : IRequest<GetVotesQueryResult> {
        public string SessionId { get; }

        public GetVotesQuery(string sessionId) {
            this.SessionId = sessionId;
        }
    }
}
