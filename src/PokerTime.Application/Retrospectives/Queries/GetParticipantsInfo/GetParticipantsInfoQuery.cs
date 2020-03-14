// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetParticipantsInfoQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetParticipantsInfo {
    using MediatR;

    public sealed class GetParticipantsInfoQuery : IRequest<ParticipantsInfoList> {
        public GetParticipantsInfoQuery(string retroId) {
            this.RetroId = retroId;
        }

        public string RetroId { get; }
    }
}
