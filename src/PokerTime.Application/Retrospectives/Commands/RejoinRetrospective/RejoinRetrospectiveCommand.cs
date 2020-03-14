// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RejoinRetrospectiveCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Commands.RejoinRetrospective {
    using MediatR;

    public sealed class RejoinRetrospectiveCommand : IRequest {
        public string SessionId { get; }

        public int ParticipantId { get; }

        public RejoinRetrospectiveCommand(string sessionId, int participantId) {
            this.SessionId = sessionId;
            this.ParticipantId = participantId;
        }
    }
}
