// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ISessionJoinedSubscriber.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.SessionJoined {
    using System.Threading.Tasks;
    using Sessions.Queries.GetParticipantsInfo;

    public interface ISessionJoinedSubscriber : ISubscriber {
        Task OnParticipantJoinedRetrospective(SessionEvent<ParticipantInfo> eventArgs);
    }
}
