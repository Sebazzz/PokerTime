// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : NoteLaneUpdatedNotification.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.NoteLaneUpdated {
    using MediatR;

    public sealed class NoteLaneUpdatedNotification : INotification {
        public string SessionId { get; }
        public int LaneId { get; }
        public int GroupId { get; }

        public NoteLaneUpdatedNotification(string sessionId, int laneId, int groupId) {
            this.SessionId = sessionId;
            this.LaneId = laneId;
            this.GroupId = groupId;
        }
    }
}
