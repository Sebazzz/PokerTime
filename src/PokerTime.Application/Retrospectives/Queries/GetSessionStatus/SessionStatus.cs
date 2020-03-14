// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatus.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetSessionStatus {
    using System;
    using System.Collections.Generic;
    using Domain.Entities;

    public sealed class SessionStatus {
        public string SessionId { get; }

        public string Title { get; }

        public bool IsViewingOtherNotesAllowed => this.Stage >= SessionStage.Discuss;
        public bool IsViewingVotesAllowed => this.Stage >= SessionStage.Voting;
        public bool IsVotingAllowed => this.Stage == SessionStage.Voting;
        public bool IsEditingNotesAllowed => this.Stage == SessionStage.Writing;
        public bool IsDeletingNotesAllowed => this.Stage == SessionStage.Writing;
        public bool IsGroupingAllowed(bool isFacilitator) => this.Stage == SessionStage.Grouping && isFacilitator;

        public RetrospectiveWorkflowStatus WorkflowStatus { get; }

        public int VotesPerLane { get; }

        public SessionStage Stage { get; }

        public SessionStatus(string sessionId, string title, SessionStage sessionStage, RetrospectiveWorkflowStatus workflowStatus, int votesPerLane) {
            this.SessionId = sessionId;
            this.Title = title;
            this.Stage = sessionStage;
            this.WorkflowStatus = workflowStatus;
            this.VotesPerLane = votesPerLane;
        }

        public SessionStatus() {
            this.SessionId = String.Empty;
            this.Title = String.Empty;
            this.Stage = SessionStage.NotStarted;
            this.WorkflowStatus = new RetrospectiveWorkflowStatus();
            this.VotesPerLane = -1;
        }
    }

    public class RetrospectiveWorkflowStatus {
        public DateTimeOffset InitiationTimestamp { get; set; }

        public int TimeLimitInMinutes { get; set; }

        public bool HasReachedTimeLimit(DateTimeOffset now) => this.InitiationTimestamp.AddMinutes(this.TimeLimitInMinutes) <= now;
        public TimeSpan GetTimeLeft(DateTimeOffset now) {
            TimeSpan result = this.InitiationTimestamp.AddMinutes(this.TimeLimitInMinutes) - now;

            return result < TimeSpan.Zero ? TimeSpan.Zero : result;
        }

        internal static RetrospectiveWorkflowStatus FromDomainWorkflowData(RetrospectiveWorkflowData workflowData) =>
            new RetrospectiveWorkflowStatus {
                InitiationTimestamp = workflowData.CurrentWorkflowInitiationTimestamp,
                TimeLimitInMinutes = workflowData.CurrentWorkflowTimeLimitInMinutes
            };
    }
}
