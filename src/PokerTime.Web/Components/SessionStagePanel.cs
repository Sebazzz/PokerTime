// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveStageText2.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Components {
    using Application.Sessions.Queries.GetSessionStatus;
    using Domain.Entities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

#nullable disable

    /// <summary>
    /// Represents a panel which will only render in the case the retrospective is in a certain stage
    /// </summary>
    public sealed class SessionStagePanel : ComponentBase {
        [Parameter]
        public SessionStage ApplicableTo { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        public SessionStatus SessionStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Framework infrastructure provides argument")]
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            if (this.SessionStatus?.Stage == this.ApplicableTo) {
                this.ChildContent.Invoke(builder);
            }
        }
    }
}
