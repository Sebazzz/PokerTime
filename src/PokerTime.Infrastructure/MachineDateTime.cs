// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : MachineDateTime.cs
//  Project         : PokerTime.Infrastructure
// ******************************************************************************

namespace PokerTime.Infrastructure {
    using System;
    using Common;

    public sealed class MachineSystemClock : ISystemClock {
        public DateTime Now => DateTime.Now;
        public DateTimeOffset CurrentTimeOffset => DateTimeOffset.Now;
    }
}
