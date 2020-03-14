// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : NoteAddedNotification.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.NoteAdded {
    using Common.Models;

    public sealed class NoteAddedNotification : RetrospectiveNotification {
        public int LaneId { get; }

        public RetrospectiveNote Note { get; }

        public NoteAddedNotification(string retroId, int laneId, RetrospectiveNote note) : base(retroId) {
            this.LaneId = laneId;
            this.Note = note;
        }
    }
}
