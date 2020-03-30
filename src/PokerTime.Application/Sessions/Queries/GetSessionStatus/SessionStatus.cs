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

        public UserStoryModel? UserStory { get; }

        public bool CanViewOwnCards => this.Stage != SessionStage.Finished && this.Stage != SessionStage.NotStarted;
        public bool CanChooseCards => this.Stage == SessionStage.Estimation;
        public bool CanViewEstimationPanel => this.Stage != SessionStage.Discussion && this.Stage != SessionStage.Finished && this.Stage != SessionStage.NotStarted;
        public bool CanViewEstimations => this.Stage == SessionStage.EstimationDiscussion || this.Stage == SessionStage.Finished;
        public bool ShowUserStoriesOverview => this.Stage == SessionStage.Finished;
        public bool IsStarted => this.Stage != SessionStage.NotStarted;

        public SessionStatus(
            string sessionId,
            string title,
            SessionStage sessionStage,
            int symbolSetId,
            UserStoryModel? currentUserStory
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
