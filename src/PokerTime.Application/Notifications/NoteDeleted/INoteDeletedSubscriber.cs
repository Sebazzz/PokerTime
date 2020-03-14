// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : INoteDeletedSubscriber.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notifications.NoteDeleted {
    using System.Threading.Tasks;

    public interface INoteDeletedSubscriber : ISubscriber {
        Task OnNoteDeleted(NoteDeletedNotification notification);
    }
}
