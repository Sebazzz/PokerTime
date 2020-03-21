// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : EstimationOverview.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Components {
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using OpenQA.Selenium;

    public class EstimationOverview {
        public EstimationOverview(IWebElement webElement) {
            this.WebElement = webElement;
        }

        public IWebElement WebElement { get; }

        public IEnumerable<CardComponent> Cards => this.WebElement.FindElementsByTestElementId("estimation-card").Select(x => new CardComponent(x));
        public IEnumerable<string> UnestimatedCards => this.WebElement.FindElementsByTestElementId("unestimated-card").Select(x => x.GetAttribute("data-participant-name"));
    }
}
