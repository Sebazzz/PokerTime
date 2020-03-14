// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetVotesQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Votes.Queries {
    using MediatR;

    public sealed class GetVotesQuery : IRequest<GetVotesQueryResult> {
        public string RetroId { get; }

        public GetVotesQuery(string retroId) {
            this.RetroId = retroId;
        }
    }
}
