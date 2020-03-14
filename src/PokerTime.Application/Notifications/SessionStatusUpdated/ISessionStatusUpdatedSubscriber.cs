// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ISessionStatusUpdatedSubscriber.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.SessionStatusUpdated {
    using System.Threading.Tasks;
    using Sessions.Queries.GetSessionStatus;

    public interface ISessionStatusUpdatedSubscriber : ISubscriber {
        Task OnSessionStatusUpdated(SessionStatus sessionStatus);
    }
}
