// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetJoinPokerSessionInfoQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetJoinPokerSessionInfo {
    using MediatR;

    public sealed class GetJoinPokerSessionInfoQuery : IRequest<JoinPokerSessionInfo?> {
#nullable disable
        public string SessionId { get; set; }
    }
}
