// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RejoinPokerSessionCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Commands.RejoinPokerSession {
    using MediatR;

    public sealed class RejoinPokerSessionCommand : IRequest {
        public string SessionId { get; }

        public int ParticipantId { get; }

        public RejoinPokerSessionCommand(string sessionId, int participantId) {
            this.SessionId = sessionId;
            this.ParticipantId = participantId;
        }
    }
}
