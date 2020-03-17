// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionOnlineList.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Components {
    using System.Collections.Generic;
    using Common;
    using OpenQA.Selenium;

    public class PokerOnlineListComponent {
        private readonly IWebDriver _webDriver;

        public PokerOnlineListComponent(IWebDriver webDriver) {
            this._webDriver = webDriver;
        }

        public IEnumerable<IWebElement> OnlineListItems => this._webDriver.FindElements(By.CssSelector("#poker-online-list span[data-participant-id]"));
        public IWebElement GetListItem(int id) => this._webDriver.FindVisibleElement(By.CssSelector($"#poker-online-list span[data-participant-id=\"{id}\"]"));
    }
}
