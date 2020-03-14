// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CastVoteCommandValidator.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Votes.Commands {
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;

    [SuppressMessage(category: "Naming",
        checkId: "CA1710:Identifiers should have correct suffix",
        Justification = "This is a validation rule set.")]
    public sealed class CastVoteCommandValidator : AbstractValidator<CastVoteCommand> {
        public CastVoteCommandValidator() {
            this.RuleFor(expression: x => x.Id);
        }
    }
}
