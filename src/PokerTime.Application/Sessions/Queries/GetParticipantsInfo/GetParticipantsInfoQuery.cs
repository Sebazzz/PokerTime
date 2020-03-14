// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetParticipantsInfoQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetParticipantsInfo {
    using MediatR;

    public sealed class GetParticipantsInfoQuery : IRequest<ParticipantsInfoList> {
        public GetParticipantsInfoQuery(string sessionId) {
            this.SessionId = sessionId;
        }

        public string SessionId { get; }
    }
}
