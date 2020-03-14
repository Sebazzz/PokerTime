// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetJoinRetrospectiveInfoQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetJoinRetrospectiveInfo {
    using MediatR;

    public sealed class GetJoinRetrospectiveInfoQuery : IRequest<JoinRetrospectiveInfo?> {
#nullable disable
        public string SessionId { get; set; }
    }
}
