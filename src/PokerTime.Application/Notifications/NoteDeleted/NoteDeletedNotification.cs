// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : NoteDeletedNotification.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.NoteDeleted {
    using MediatR;

    public class NoteDeletedNotification : INotification {
        public NoteDeletedNotification(string sessionId, int laneId, in int noteId) {
            this.SessionId = sessionId;
            this.LaneId = laneId;
            this.NoteId = noteId;
        }

        public string SessionId { get; }
        public int LaneId { get; }
        public int NoteId { get; }
    }
}
