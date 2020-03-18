// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateDiscussionStageCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SessionWorkflows.Commands {
    using MediatR;

    public sealed class InitiateDiscussionStageCommand : AbstractStageCommand, IRequest {
        public string? UserStoryTitle { get; set; }

        public bool IsReestimation { get; set; }
    }

}
