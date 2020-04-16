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
    using Application.Notifications.SessionStatusUpdated;
    using Application.Sessions.Queries.GetSessionStatus;
    using Components;
    using Components.Layout;
    using Domain.ValueObjects;
    using Microsoft.AspNetCore.Components;

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "In-app callbacks")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Set by framework")]
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "We catch, log and display.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Needed for DI")]
    public abstract class PokerSessionLobbyBase : MediatorComponent, ISessionStatusUpdatedSubscriber, IDisposable {
        public Guid UniqueId { get; } = Guid.NewGuid();

#nullable disable

        [Inject]
        public INotificationSubscription<ISessionStatusUpdatedSubscriber> SessionStatusUpdatedSubscription { get; set; }


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

        public SessionStatus SessionStatus { get; set; } = null;

        public SessionIdentifier SessionIdObject { get; set; }

        protected bool HasLoaded { get; private set; }

#nullable restore

        protected override void OnInitialized() {
            this.SessionIdObject = new SessionIdentifier(this.SessionId);

            this.SessionStatusUpdatedSubscription.Subscribe(this);

            base.OnInitialized();
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                this.SessionStatusUpdatedSubscription.Unsubscribe(this);
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override async Task OnInitializedAsync() {
            CurrentParticipantModel currentParticipant = await this.CurrentParticipantService.GetParticipant();

            if (!currentParticipant.IsAuthenticated) {
                this.NavigationManager.NavigateTo("/pokertime-session/" + this.SessionId + "/join");
                return;
            }

            this.CurrentParticipant = currentParticipant;

            try {
                this.SessionStatus = await this.Mediator.Send(new GetSessionStatusQuery(this.SessionId));
                this.Layout?.Update(new PokerSessionLayoutInfo(this.SessionStatus.Title, this.SessionStatus.Stage));
            }
            catch (NotFoundException) {
                this.SessionStatus = null;
            }
            finally {
                this.HasLoaded = true;
            }
        }

        public Task OnSessionStatusUpdated(SessionStatus sessionStatus) {
            if (sessionStatus.SessionId != this.SessionId) {
                return Task.CompletedTask;
            }

            this.SessionStatus = sessionStatus;

            this.InvokeAsync(() => {
                this.Layout?.Update(new PokerSessionLayoutInfo(sessionStatus.Title, sessionStatus.Stage));
                this.StateHasChanged();
            });

            return Task.CompletedTask;
        }
    }
}
