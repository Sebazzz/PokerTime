// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSessionStatusQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetSessionStatus {
    using MediatR;

    public sealed class GetSessionStatusQuery : IRequest<SessionStatus> {
        public string SessionId { get; }

        public GetSessionStatusQuery(string sessionId) {
            this.SessionId = sessionId;
        }
    }
}
