// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : INoteAddedSubscriber.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.NoteAdded {
    using System.Threading.Tasks;

    public interface INoteAddedSubscriber : ISubscriber {
        Task OnNoteAdded(NoteAddedNotification notification);
    }
}
