// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : DeleteNoteGroupCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.NoteGroups.Commands {
    using MediatR;

    public sealed class DeleteNoteGroupCommand : IRequest {
        public DeleteNoteGroupCommand(string sessionId, int id) {
            this.SessionId = sessionId;
            this.Id = id;
        }

        public string SessionId { get; }

        public int Id { get; }
    }
}
