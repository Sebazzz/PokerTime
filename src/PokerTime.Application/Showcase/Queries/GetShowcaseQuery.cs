// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetShowcaseQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Showcase.Queries {
    using Common.Behaviours;
    using MediatR;

    public sealed class GetShowcaseQuery : IRequest<GetShowcaseQueryResult>, ILockFreeRequest {
        public string SessionId { get; }

        public GetShowcaseQuery(string sessionId) {
            this.SessionId = sessionId;
        }
    }
}
