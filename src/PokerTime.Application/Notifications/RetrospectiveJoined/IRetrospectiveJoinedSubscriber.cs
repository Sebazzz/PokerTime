// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IRetrospectiveJoinedSubscriber.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.RetrospectiveJoined {
    using System.Threading.Tasks;
    using Retrospectives.Queries.GetParticipantsInfo;

    public interface IRetrospectiveJoinedSubscriber : ISubscriber {
        Task OnParticipantJoinedRetrospective(RetrospectiveEvent<ParticipantInfo> eventArgs);
    }
}
