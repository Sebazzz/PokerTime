// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : UserStory.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Entities {
    using System.Collections.Generic;

    /// <summary>
    /// User stories are estimated by team members
    /// </summary>
    public class UserStory {
        private ICollection<Estimation>? _estimations;

        public int Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public Session Session { get; set; }
        public int SessionId { get; set; }

        public ICollection<Estimation> Estimations => this._estimations ??= new List<Estimation>();

#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public string? Title { get; set; }
    }
}
