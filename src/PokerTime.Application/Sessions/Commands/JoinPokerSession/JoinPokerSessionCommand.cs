// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : JoinPokerSessionCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Commands.JoinPokerSession {
    using MediatR;
    using Queries.GetParticipantsInfo;

#nullable disable

    public sealed class JoinPokerSessionCommand : IRequest<ParticipantInfo> {
        public string Name { get; set; }
        public string Color { get; set; }

        public string Passphrase { get; set; }

        public bool JoiningAsFacilitator { get; set; }
        public string SessionId { get; set; }

        public override string ToString() => $"[{nameof(JoinPokerSessionCommand)}] Join retro {this.SessionId} as {this.Name} (facilitator: {this.JoiningAsFacilitator})";
    }
}
