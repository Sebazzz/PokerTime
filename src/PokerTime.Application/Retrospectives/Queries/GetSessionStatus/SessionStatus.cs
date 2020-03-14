// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatus.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetSessionStatus {
    using System;
    using Domain.Entities;

    public sealed class SessionStatus {
        public string SessionId { get; }

        public string Title { get; }

        public SessionStage Stage { get; }

        public SessionStatus(string sessionId, string title, SessionStage sessionStage) {
            this.SessionId = sessionId;
            this.Title = title;
            this.Stage = sessionStage;
        }

        public SessionStatus() {
            this.SessionId = String.Empty;
            this.Title = String.Empty;
            this.Stage = SessionStage.NotStarted;
        }
    }
}
