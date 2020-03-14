// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IOwnedByParticipant.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Abstractions {
    public interface IOwnedByParticipant {
        int ParticipantId { get; }
    }
}
