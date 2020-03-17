// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PlayCardCommandValidator.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Poker.Commands {
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;

    [SuppressMessage(category: "Naming",
        checkId: "CA1710:Identifiers should have correct suffix",
        Justification = "This is a validation rule set.")]
    public sealed class PlayCardCommandValidator : AbstractValidator<PlayCardCommand> {
        public PlayCardCommandValidator() {
            this.RuleFor(x => x.UserStoryId).NotEmpty();
            this.RuleFor(x => x.SymbolId).NotEmpty();
        }
    }
}
