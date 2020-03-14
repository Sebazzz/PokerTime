// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : NoteComponent.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Components {
    using Common;
    using OpenQA.Selenium;

    public sealed class NoteComponent {
        public NoteComponent(IWebElement webElement) {
            this.WebElement = webElement;
        }

        public IWebElement WebElement { get; }
        public int Id => this.WebElement.GetAttribute<int>("data-id");
        public IWebElement Input => this.WebElement.FindElement(By.ClassName("textarea"));
        public IWebElement Content => this.WebElement.FindElement(By.ClassName("note__content"));
        public IWebElement DeleteButton => this.WebElement.FindElement(By.ClassName("note__delete-icon"));
    }
}
