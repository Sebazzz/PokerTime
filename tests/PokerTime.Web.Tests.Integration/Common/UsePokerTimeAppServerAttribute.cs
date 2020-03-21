// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : UseReturnAppServerAttribute.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

[assembly: PokerTime.Web.Tests.Integration.Common.UseReturnAppServer]

namespace PokerTime.Web.Tests.Integration.Common {
    using System;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    /// <summary>
    /// Assembly-wide attribute that will set-up the server and database, and also tear it down after the test
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class UseReturnAppServerAttribute : Attribute, ITestAction {
        public void BeforeTest(ITest test) {
            TestContext.WriteLine("Setting up server and initial webdriver");

            ServerInstance = new PokerTimeAppFactory();
            ServerInstance.InitializeBaseData();
        }

        public void AfterTest(ITest test) {
            TestContext.WriteLine("Tearing down server and core webdriver");

            ServerInstance?.Dispose();
            ServerInstance = null;
        }

        public ActionTargets Targets => ActionTargets.Suite;

        internal static PokerTimeAppFactory ServerInstance;
    }
}
