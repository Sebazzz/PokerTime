// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetParticipantQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetParticipant {
    using GetParticipantsInfo;
    using MediatR;

    public sealed class GetParticipantQuery : IRequest<ParticipantInfo?> {
        public string Name { get; }
        public string SessionId { get; }

        public GetParticipantQuery(string name, string sessionId) {
            this.Name = name;
            this.SessionId = sessionId;
        }
    }
}
