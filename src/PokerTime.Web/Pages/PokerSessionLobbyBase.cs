// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PokerSessionLobbyBase.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Pages {
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Abstractions;
    using Application.Common.Models;
    using Application.Notifications;
    using Application.Notifications.RetrospectiveStatusUpdated;
    using Application.Retrospectives.Queries.GetRetrospectiveStatus;
    using Components;
    using Components.Layout;
    using Domain.ValueObjects;
    using Microsoft.AspNetCore.Components;

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "In-app callbacks")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Set by framework")]
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "We catch, log and display.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Needed for DI")]
    public abstract class PokerSessionLobbyBase : MediatorComponent, IRetrospectiveStatusUpdatedSubscriber, IDisposable {
        public Guid UniqueId { get; } = Guid.NewGuid();

#nullable disable

        [Inject]
        public INotificationSubscription<IRetrospectiveStatusUpdatedSubscriber> RetrospectiveStatusUpdatedSubscription { get; set; }


        [Inject]
        public ICurrentParticipantService CurrentParticipantService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Route parameter
        /// </summary>
        [Parameter]
        public string SessionId { get; set; }

        [CascadingParameter]
        public IPokerSessionLayout Layout { get; set; }

        public CurrentParticipantModel CurrentParticipant { get; set; }

        public RetrospectiveStatus RetrospectiveStatus { get; set; } = null;

        public SessionIdentifier SessionIdObject { get; set; }

        protected bool HasLoaded { get; private set; }

#nullable restore

        protected override void OnInitialized() {
            this.SessionIdObject = new SessionIdentifier(this.SessionId);

            this.RetrospectiveStatusUpdatedSubscription.Subscribe(this);

            base.OnInitialized();
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                this.RetrospectiveStatusUpdatedSubscription.Unsubscribe(this);
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override async Task OnInitializedAsync() {
            CurrentParticipantModel currentParticipant = await this.CurrentParticipantService.GetParticipant();

            if (!currentParticipant.IsAuthenticated) {
                this.NavigationManager.NavigateTo("/session/" + this.SessionId + "/join");
                return;
            }

            this.CurrentParticipant = currentParticipant;

            try {
                this.RetrospectiveStatus = await this.Mediator.Send(new GetRetrospectiveStatusQuery(this.SessionId));
                this.Layout?.Update(new PokerSessionLayoutInfo(this.RetrospectiveStatus.Title, this.RetrospectiveStatus.Stage));
            }
            catch (NotFoundException) {
                this.RetrospectiveStatus = null;
            }
            finally {
                this.HasLoaded = true;
            }
        }

        public Task OnRetrospectiveStatusUpdated(RetrospectiveStatus retrospectiveStatus) {
            if (this.RetrospectiveStatus?.SessionId != this.SessionId) {
                return Task.CompletedTask;
            }

            this.RetrospectiveStatus = retrospectiveStatus;
            this.Layout?.Update(new PokerSessionLayoutInfo(retrospectiveStatus.Title, retrospectiveStatus.Stage));
            this.StateHasChanged();

            return Task.CompletedTask;
        }
    }
}
