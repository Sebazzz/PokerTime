// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : JoinPokerSessionInfo.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetJoinPokerSessionInfo {

    public sealed class JoinPokerSessionInfo {
        public string Title { get; }

        public bool NeedsParticipantPassphrase { get; }
        public bool IsStarted { get; }
        public bool IsFinished { get; }


        public JoinPokerSessionInfo(string title, bool needsParticipantPassphrase, bool isStarted, bool isFinished) {
            this.NeedsParticipantPassphrase = needsParticipantPassphrase;
            this.IsStarted = isStarted;
            this.IsFinished = isFinished;
            this.Title = title;
        }
    }
}
