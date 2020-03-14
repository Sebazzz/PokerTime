// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionRolePanel.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Components {
    using Application.Common.Models;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

#nullable disable

    /// <summary>
    /// Represents a panel which will only render one content or the other based on the role of the user
    /// </summary>
    public sealed class SessionRolePanel : ComponentBase {
        [Parameter]
        public RenderFragment Facilitator { get; set; }

        [Parameter]
        public RenderFragment Participant { get; set; }

        [CascadingParameter]
        public CurrentParticipantModel CurrentParticipantInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Framework infrastructure provides argument")]
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            RenderFragment applicableRenderFragment = this.CurrentParticipantInfo.IsFacilitator ? this.Facilitator : this.Participant;

            applicableRenderFragment.Invoke(builder);
        }
    }
}
