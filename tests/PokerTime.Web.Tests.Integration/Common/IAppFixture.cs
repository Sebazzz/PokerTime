﻿// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IAppFixture.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Common {
    using Microsoft.Extensions.DependencyInjection;

    public interface IAppFixture {
        ReturnAppFactory App { get; set; }
        void OnInitialized() { }
    }
}
