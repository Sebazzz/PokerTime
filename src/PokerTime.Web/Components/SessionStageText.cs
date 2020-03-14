// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveStageTitle.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Components {
    using Application.Sessions.Queries.GetSessionStatus;
    using Domain.Entities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

#nullable disable

    public sealed class SessionStageText : ComponentBase {
        [Parameter]
        public SessionStage ApplicableTo { get; set; }

        [Parameter]
        public string Text { get; set; }

        [CascadingParameter]
        public SessionStatus SessionStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Framework infrastructure provides argument")]
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            if (this.SessionStatus?.Stage == this.ApplicableTo) {
                builder.AddContent(0, this.Text);
            }
        }
    }
}
