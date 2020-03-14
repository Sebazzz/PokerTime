// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : Class1.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.NoteMoved {
    using System.Threading.Tasks;

    public interface INoteMovedSubscriber : ISubscriber {
        Task OnNoteMoved(NoteMovedNotification notification);
    }
}
