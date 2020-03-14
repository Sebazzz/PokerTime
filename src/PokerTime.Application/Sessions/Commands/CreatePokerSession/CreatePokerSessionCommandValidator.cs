// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CreatePokerSessionCommandValidator.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Commands.CreatePokerSession {
    using FluentValidation;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "This is a validation rule set.")]
    public sealed class CreatePokerSessionCommandValidator : AbstractValidator<CreatePokerSessionCommand> {
        public CreatePokerSessionCommandValidator() {
            this.RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
            this.RuleFor(x => x.Passphrase).MaximumLength(512);
            this.RuleFor(x => x.FacilitatorPassphrase).NotEmpty().MaximumLength(512);
        }
    }
}
