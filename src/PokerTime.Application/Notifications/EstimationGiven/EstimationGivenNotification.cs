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

        public string SessionId { get; }

        public EstimationGivenNotification(string sessionId, EstimationModel estimation) {
            this.SessionId = sessionId;
            this.Estimation = estimation;
        }
    }
}
