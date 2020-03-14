// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : DeleteNoteCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notes.Commands.DeleteNote {
    using MediatR;

    public sealed class DeleteNoteCommand : IRequest {
        public string RetroId { get; }

        public int NoteId { get; }

        public DeleteNoteCommand(string retroId, int noteId) {
            this.RetroId = retroId;
            this.NoteId = noteId;
        }
    }
}
