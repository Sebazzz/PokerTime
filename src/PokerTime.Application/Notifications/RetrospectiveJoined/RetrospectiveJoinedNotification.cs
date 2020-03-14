﻿// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveJoinedNotification.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.RetrospectiveJoined {
    using Retrospectives.Queries.GetParticipantsInfo;

    public sealed class RetrospectiveJoinedNotification : RetrospectiveNotification {
        public ParticipantInfo ParticipantInfo { get; }

        public RetrospectiveJoinedNotification(string retroId, ParticipantInfo participantInfo) : base(retroId) {
            this.ParticipantInfo = participantInfo;
        }
    }
}
