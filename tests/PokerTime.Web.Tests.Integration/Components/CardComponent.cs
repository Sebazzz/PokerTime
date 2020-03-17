// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CardComponent.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Components {
    using System;
    using Common;
    using OpenQA.Selenium;

    public class CardComponent {
        public CardComponent(IWebElement webElement) {
            this.WebElement = webElement;
        }

        public IWebElement WebElement { get; }

        public int Id => this.WebElement.GetAttribute<int>("data-id");
        public int SymbolId => this.WebElement.GetAttribute<int>("data-symbol-id");
        public string SymbolText => this.WebElement.FindElement(By.ClassName("poker-card__symbol")).Text.Trim();
        public bool IsChoosable => this.WebElement.GetAttribute("class").Contains("poker-card--choosable", StringComparison.OrdinalIgnoreCase);

        public void Click() => this.WebElement.Click();
    }
}
