// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ISystemClock.cs
//  Project         : PokerTime.Common
// ******************************************************************************

namespace PokerTime.Common
{
    using System;

    public interface ISystemClock
    {
        DateTime Now { get; }

        DateTimeOffset CurrentTimeOffset { get; }
    }
}
