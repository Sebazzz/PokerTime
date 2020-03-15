// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : EstimationGivenNotification.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.EstimationGiven {
    using Common.Models;
    using MediatR;

    public sealed class EstimationGivenNotification : INotification {
        public EstimationModel Estimation { get; }

        public EstimationGivenNotification(EstimationModel estimation) {
            this.Estimation = estimation;
        }
    }
}
