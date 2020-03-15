// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : EstimationOverviewBase.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Components {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Common.Models;
    using Application.Estimations.Queries;
    using Application.Notifications.EstimationGiven;
    using Application.Sessions.Queries.GetSessionStatus;
    using Microsoft.AspNetCore.Components;

    public abstract class EstimationOverviewBase : MediatorComponent, IEstimationGivenSubscriber {
        private int _userStoryId;

#nullable disable
        [CascadingParameter]
        public SessionStatus SessionStatus { get; set; }

        [Inject]
        public EstimationGivenNotificationDispatcher EstimationGivenNotificationDispatcher { get; set; }

        public Guid UniqueId { get; } = Guid.NewGuid();


#nullable restore

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        protected IDictionary<int, EstimationModel> Estimations { get; set; } = new Dictionary<int, EstimationModel>();

        public Task OnEstimationGiven(EstimationModel estimation) {
            this.InvokeAsync(() => {
                this.Estimations[estimation.ParticipantId] = estimation;

                this.StateHasChanged();
            });

            return Task.CompletedTask;
        }

        protected override void OnInitialized() {
            base.OnInitialized();

            this.EstimationGivenNotificationDispatcher.Subscribe(this);
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
    }
}
