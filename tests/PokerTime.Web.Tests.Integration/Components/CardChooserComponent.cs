// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CardChooserComponent.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Components {
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using OpenQA.Selenium;

    public class CardChooserComponent {
        public CardChooserComponent(IWebElement webElement) {
            this.WebElement = webElement;
        }

        public IWebElement WebElement { get; }

        public IEnumerable<CardComponent> Cards => this.WebElement.FindElementsByTestElementId("user-poker-card").Select(x => new CardComponent(x));
    }
}
