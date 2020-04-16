// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : EstimationOverviewBase.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Components {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Common.Models;
    using Application.Estimations.Queries;
    using Application.Notifications;
    using Application.Notifications.EstimationGiven;
    using Application.Notifications.SessionJoined;
    using Application.Sessions.Queries.GetParticipantsInfo;
    using Application.Sessions.Queries.GetSessionStatus;
    using Microsoft.AspNetCore.Components;

    public abstract class EstimationOverviewBase : MediatorComponent, IDisposable, IEstimationGivenSubscriber, ISessionJoinedSubscriber {
        private int _userStoryId;

#nullable disable
        [CascadingParameter]
        public SessionStatus SessionStatus { get; set; }

        [Inject]
        public INotificationSubscription<IEstimationGivenSubscriber> EstimationGivenSubscriber { get; set; }

        [Inject]
        public INotificationSubscription<ISessionJoinedSubscriber> SessionJoinedSubscriber { get; set; }

        public Guid UniqueId { get; } = Guid.NewGuid();

        private ParticipantsInfoList ParticipantList { get; set; }

        protected IEnumerable<string> ParticipantsWithoutEstimation {
            get {
                if (this.ParticipantList == null) {
                    yield break;
                }

                foreach (ParticipantInfo participant in this.ParticipantList.Participants) {
                    if (!this.Estimations.ContainsKey(participant.Id)) {
                        yield return participant.Name;
                    }
                }
            }
        }

#nullable restore

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        protected IDictionary<int, EstimationModel> Estimations { get; set; } = new Dictionary<int, EstimationModel>();

        public Task OnEstimationGiven(EstimationGivenNotification notification) {
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            if (notification.SessionId != this.SessionStatus.SessionId) {
                return Task.CompletedTask;
            }

            this.InvokeAsync(() => {
                this.Estimations[notification.Estimation.ParticipantId] = notification.Estimation;

                this.StateHasChanged();
            });

            return Task.CompletedTask;
        }

        protected override void OnInitialized() {
            base.OnInitialized();

            this.EstimationGivenSubscriber.Subscribe(this);
            this.SessionJoinedSubscriber.Subscribe(this);
        }

        protected override async Task OnInitializedAsync() {
            this.ParticipantList = await this.Mediator.Send(new GetParticipantsInfoQuery(this.SessionStatus.SessionId));
            Debug.Assert(this.ParticipantList != null);
        }

        protected override Task OnParametersSetAsync() {
            if (this.SessionStatus.UserStory?.Id == this._userStoryId) {
                return base.OnParametersSetAsync();
            }

            if (this.SessionStatus.UserStory == null) {
                this._userStoryId = 0;
                return base.OnParametersSetAsync();
            }

            int userStoryId = this.SessionStatus.UserStory.Id;
            async Task LoadCore() {
                await base.OnParametersSetAsync();

                GetEstimationsQueryResponse estimationsResponse = await this.Mediator.Send(
                    new GetEstimationsQuery(this.SessionStatus.SessionId, userStoryId));

                this.Estimations = estimationsResponse.Estimations.ToDictionary(x => x.ParticipantId, x => x);
                this._userStoryId = userStoryId;
            }

            return LoadCore();
        }

        public Task OnParticipantJoinedRetrospective(SessionEvent<ParticipantInfo> eventArgs) {
            if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

            if (eventArgs.SessionId != this.SessionStatus?.SessionId) {
                return Task.CompletedTask;
            }

            ParticipantInfo participantInfo = eventArgs.Argument;

            return this.InvokeAsync(() => {
                this.ParticipantList.Participants.Add(participantInfo);
                this.ParticipantList.Participants.Sort((a, b) => StringComparer.CurrentCulture.Compare(a.Name, b.Name));

                this.StateHasChanged();
            });
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                this.EstimationGivenSubscriber?.Unsubscribe(this);
                this.SessionJoinedSubscriber?.Unsubscribe(this);
            }
        }

        public void Dispose() {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
