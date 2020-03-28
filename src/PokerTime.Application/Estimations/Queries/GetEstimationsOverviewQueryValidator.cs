// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsOverviewQueryValidator.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;

    [SuppressMessage(category: "Naming", checkId: "CA1710:Identifiers should have correct suffix", Justification = "This is a validation rule set.")]
    public sealed class GetEstimationsOverviewQueryValidator : AbstractValidator<GetEstimationsOverviewQuery> {
        public GetEstimationsOverviewQueryValidator() {
            this.RuleFor(x => x.SessionId).NotEmpty();
        }
    }
}
