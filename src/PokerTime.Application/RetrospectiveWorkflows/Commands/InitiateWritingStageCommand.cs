// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateWritingStageCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveWorkflows.Commands {
    using MediatR;

    public sealed class InitiateWritingStageCommand : AbstractTimedStageCommand, IRequest {
    }
}
