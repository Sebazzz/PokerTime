// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : VoteChange.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.VoteChanged {
    using Common.Models;

    public sealed class VoteChange {
        public VoteModel Vote { get; }

        public string SessionId { get; }

        public VoteMutationType Mutation { get; }

        public VoteChange(string sessionId, VoteModel vote, VoteMutationType mutation) {
            this.SessionId = sessionId;
            this.Vote = vote;
            this.Mutation = mutation;
        }
    }
}
