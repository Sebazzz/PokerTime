// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : INotificationSubscription.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications {
    public interface INotificationSubscription<in TSubscriber> where TSubscriber : ISubscriber {
        void Subscribe(TSubscriber subscriber);

        void Unsubscribe(TSubscriber subscriber);
    }
}
