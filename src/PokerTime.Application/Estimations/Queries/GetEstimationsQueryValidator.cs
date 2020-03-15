// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsQueryValidator.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;

    [SuppressMessage(category: "Naming",
        checkId: "CA1710:Identifiers should have correct suffix",
        Justification = "This is a validation rule set.")]
    public sealed class GetEstimationsQueryValidator : AbstractValidator<GetEstimationsQuery> {
        public GetEstimationsQueryValidator() {
            this.RuleFor(x => x.SessionId).NotNull();
            this.RuleFor(x => x.UserStoryId).NotEmpty();
        }
    }
}
