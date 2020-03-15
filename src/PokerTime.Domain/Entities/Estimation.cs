// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : Estimation.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Entities {
    public class Estimation {
        public int Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public int UserStoryId { get; set; }

        public UserStory UserStory { get; set; }

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }

#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public Symbol? Symbol { get; set; }
    }
}
