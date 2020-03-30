// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PokerSessionLobby.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Pages {
    using System.Threading;
    using Common;
    using Components;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    public sealed class PokerSessionLobby : PageObject {
        public IWebElement WorkflowContinueButton => this.WebDriver.FindElementByTestElementId("workflow-continue-button");
        public IWebElement WorkflowEndButton => this.WebDriver.FindElementByTestElementId("workflow-end-button");

        public IWebElement WaitForStartMessageElement => this.WebDriver.FindElementByTestElementId("wait-for-start-message");
        public IWebElement CardChooserElement => this.WebDriver.FindElementByTestElementId("card-chooser");
        public IWebElement SessionFinishedElement => this.WebDriver.FindElementByTestElementId("session-finished-message");
        public IWebElement EstimationOverviewElement => this.WebDriver.FindElementByTestElementId("estimation-overview");

        public IWebElement UserStoryTitleInput => this.WebDriver.FindElement(By.Id("pokertime-userstory-title"));

        public CardChooserComponent CardChooser => new CardChooserComponent(this.CardChooserElement);
        public EstimationOverview EstimationOverview => new EstimationOverview(this.EstimationOverviewElement);

        public void InvokeContinueWorkflow() {
            this.WorkflowContinueButton.Click();

            // Insert sleep for AppVeyor and slower CI
            Thread.Sleep(1000);
        }

        public void InvokeEndWorkflow() {
            this.WorkflowEndButton.Click();

            // Insert sleep for AppVeyor and slower CI
            Thread.Sleep(1000);
        }

        public void SetUserStoryTitle(string title) =>
            new Actions(this.WebDriver)
                .Click(this.UserStoryTitleInput)
                .SendKeys(title)
                .SendKeys(Keys.Home)
                .Perform();
    }
}
