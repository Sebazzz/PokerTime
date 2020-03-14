// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateVotingStageCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveWorkflows.Commands {
    using MediatR;

    public sealed class InitiateVotingStageCommand : AbstractTimedStageCommand, IRequest {
        public int VotesPerGroup { get; set; }
    }
}
