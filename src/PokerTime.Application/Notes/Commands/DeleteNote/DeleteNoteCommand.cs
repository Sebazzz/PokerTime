// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : DeleteNoteCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notes.Commands.DeleteNote {
    using MediatR;

    public sealed class DeleteNoteCommand : IRequest {
        public string SessionId { get; }

        public int NoteId { get; }

        public DeleteNoteCommand(string sessionId, int noteId) {
            this.SessionId = sessionId;
            this.NoteId = noteId;
        }
    }
}
