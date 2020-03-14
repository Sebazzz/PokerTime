// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : Name.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Common {
    using System;
    using PokerTime.Common;

    public static class Name {
        public static string Create() => Guid.NewGuid().ToString("N", Culture.Invariant);
    }
}
