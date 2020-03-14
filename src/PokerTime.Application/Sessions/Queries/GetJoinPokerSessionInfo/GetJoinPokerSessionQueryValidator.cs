// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetJoinPokerSessionInfoQueryValidator.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetJoinPokerSessionInfo {
    using FluentValidation;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "This is a validation rule set.")]
    public sealed class GetJoinPokerSessionInfoQueryValidator : AbstractValidator<GetJoinPokerSessionInfoQuery> {
        public GetJoinPokerSessionInfoQueryValidator() {
            this.RuleFor(x => x.SessionId).NotEmpty();
        }
    }
}
