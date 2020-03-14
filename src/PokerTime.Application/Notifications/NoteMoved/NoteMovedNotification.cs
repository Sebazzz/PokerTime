using System;
using System.Collections.Generic;
using System.Text;

namespace PokerTime.Application.Notifications.NoteMoved {
    using MediatR;

    public sealed class NoteMovedNotification : INotification {
        public string SessionId { get; }
        public int LaneId { get; }
        public int NoteId { get; }
        public int? GroupId { get; }

        public NoteMovedNotification(string sessionId, int laneId, int noteId, int? groupId) {
            this.SessionId = sessionId;
            this.LaneId = laneId;
            this.NoteId = noteId;
            this.GroupId = groupId;
        }
    }
}
