// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatus.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetSessionStatus {
    using System;
    using Common.Models;
    using Domain.Entities;

    public sealed class SessionStatus {
        public string SessionId { get; }

        public int SymbolSetId { get; }

        public string Title { get; }

        public SessionStage Stage { get; }

        public CurrentUserStoryModel? UserStory { get; }

        public bool CanChooseCards => this.Stage == SessionStage.Estimation;
        public bool CanViewEstimationPanel => this.Stage != SessionStage.Discussion;
        public bool CanViewEstimations => this.Stage == SessionStage.EstimationDiscussion;
        public bool IsStarted => this.Stage != SessionStage.NotStarted;

        public SessionStatus(
            string sessionId,
            string title,
            SessionStage sessionStage,
            int symbolSetId,
            CurrentUserStoryModel? currentUserStory
        ) {
            this.SessionId = sessionId;
            this.Title = title;
            this.SymbolSetId = symbolSetId;
            this.Stage = sessionStage;
            this.UserStory = currentUserStory;
        }

        public SessionStatus() {
            this.SessionId = String.Empty;
            this.Title = String.Empty;
            this.Stage = SessionStage.NotStarted;
        }
    }
}
