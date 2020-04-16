// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IEstimationGivenSubscriber.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.EstimationGiven {
    using System.Threading.Tasks;

    public interface IEstimationGivenSubscriber : ISubscriber {
        Task OnEstimationGiven(EstimationGivenNotification notification);
    }
}
