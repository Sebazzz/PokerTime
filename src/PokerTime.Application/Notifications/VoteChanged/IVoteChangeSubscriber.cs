// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IVoteChangeSubscriber.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.VoteChanged {
    using System.Threading.Tasks;

    public interface IVoteChangeSubscriber : ISubscriber {
        Task OnVoteChange(VoteChange notification);
    }
}
