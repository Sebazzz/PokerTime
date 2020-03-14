// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : AddNoteGroupCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.NoteGroups.Commands {
    using Common.Models;
    using Domain.Entities;
    using MediatR;

    public sealed class AddNoteGroupCommand : IRequest<RetrospectiveNoteGroup> {
        public string SessionId { get; }
        public int LaneId { get; }

        public AddNoteGroupCommand(string sessionId, int laneId) {
            this.SessionId = sessionId;
            this.LaneId = laneId;
        }
        public override string ToString() => $"[{nameof(AddNoteGroupCommand)}] {this.SessionId} on lane {(KnownNoteLane)this.LaneId}";
    }
}
