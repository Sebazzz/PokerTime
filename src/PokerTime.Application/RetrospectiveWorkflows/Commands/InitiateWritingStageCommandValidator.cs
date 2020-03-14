// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateWritingStageCommandValidator.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveWorkflows.Commands {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "This is a validation rule set.")]
    public sealed class InitiateWritingStageCommandValidator : AbstractTimedStageCommandValidator<InitiateWritingStageCommand> { }
}
