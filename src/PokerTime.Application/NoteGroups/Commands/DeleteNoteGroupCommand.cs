// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : DeleteNoteGroupCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.NoteGroups.Commands {
    using MediatR;

    public sealed class DeleteNoteGroupCommand : IRequest {
        public DeleteNoteGroupCommand(string retroId, int id) {
            this.RetroId = retroId;
            this.Id = id;
        }

        public string RetroId { get; }

        public int Id { get; }
    }
}
