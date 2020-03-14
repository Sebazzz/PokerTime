// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : AddNoteCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notes.Commands.AddNote {
    using Common.Models;
    using MediatR;

    public sealed class AddNoteCommand : IRequest<RetrospectiveNote> {
        public string SessionId { get; }

        public int LaneId { get; }

        public AddNoteCommand(string sessionId, int laneId) {
            this.SessionId = sessionId;
            this.LaneId = laneId;
        }

        public override string ToString() => $"[{nameof(AddNoteCommand)}] SessionId: {this.SessionId}; LaneId: {this.LaneId}";
    }
}
