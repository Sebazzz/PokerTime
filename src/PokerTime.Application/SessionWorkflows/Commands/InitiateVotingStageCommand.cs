// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateVotingStageCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SessionWorkflows.Commands {
    using MediatR;

    public sealed class InitiateVotingStageCommand : AbstractTimedStageCommand, IRequest {
        public int VotesPerGroup { get; set; }
    }
}
