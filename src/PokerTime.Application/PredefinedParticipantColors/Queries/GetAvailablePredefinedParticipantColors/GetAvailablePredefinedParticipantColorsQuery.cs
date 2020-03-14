// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ParticipantColor.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.PredefinedParticipantColors.Queries.GetAvailablePredefinedParticipantColors {
    using System.Collections.Generic;
    using MediatR;

    public class GetAvailablePredefinedParticipantColorsQuery : IRequest<IList<AvailableParticipantColorModel>> {
        public string RetrospectiveId { get; }

        public GetAvailablePredefinedParticipantColorsQuery(string retrospectiveId) {
            this.RetrospectiveId = retrospectiveId;
        }

        public override string ToString() => $"[{nameof(GetAvailablePredefinedParticipantColorsQuery)}] {this.RetrospectiveId}";
    }

}
