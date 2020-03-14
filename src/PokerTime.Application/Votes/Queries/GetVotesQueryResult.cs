// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveVoteStatus.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Votes.Queries {
    public sealed class GetVotesQueryResult {
        public RetrospectiveVoteStatus VoteStatus { get; }

        public GetVotesQueryResult(RetrospectiveVoteStatus voteStatus) {
            this.VoteStatus = voteStatus;
        }
    }
}
