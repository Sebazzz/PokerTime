// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveOnlineList.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Components {
    using System.Collections.Generic;
    using Common;
    using OpenQA.Selenium;

    public class RetrospectiveOnlineListComponent {
        private readonly IWebDriver _webDriver;

        public RetrospectiveOnlineListComponent(IWebDriver webDriver) {
            this._webDriver = webDriver;
        }

        public IEnumerable<IWebElement> OnlineListItems => this._webDriver.FindElements(By.CssSelector("#retrospective-online-list span[data-participant-id]"));
        public IWebElement GetListItem(int id) => this._webDriver.FindVisibleElement(By.CssSelector($"#retrospective-online-list span[data-participant-id=\"{id}\"]"));
    }
}
