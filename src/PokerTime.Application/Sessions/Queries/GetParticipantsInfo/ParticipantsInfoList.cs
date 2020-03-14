// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ParticipantsInfoList.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetParticipantsInfo {
    using System.Collections.Generic;

    public class ParticipantsInfoList {
        public List<ParticipantInfo> Participants { get; } = new List<ParticipantInfo>();
    }
}
