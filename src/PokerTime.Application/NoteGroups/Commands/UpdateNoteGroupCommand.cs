// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : UpdateNoteGroupCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.NoteGroups.Commands {
    using Domain.Entities;
    using MediatR;

    public sealed class UpdateNoteGroupCommand : IRequest {
        public string SessionId { get; }

        public int Id { get; }

        public string Name { get; }

        public UpdateNoteGroupCommand(string sessionId, int id, string name) {
            this.SessionId = sessionId;
            this.Id = id;
            this.Name = name;
        }
        public override string ToString() => $"[{nameof(AddNoteGroupCommand)}] {this.SessionId} on lane #{(KnownNoteLane)this.Id} - content: {this.Name}";
    }
}
