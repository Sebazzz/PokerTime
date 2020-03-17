﻿// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PokerSessionLobby.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Pages {
    using System.Collections.ObjectModel;
    using System.Threading;
    using Common;
    using Components;
    using OpenQA.Selenium;

    public sealed class PokerSessionLobby : PageObject {
        public IWebElement WorkflowContinueButton => this.WebDriver.FindElementByTestElementId("workflow-continue-button");

        public IWebElement WaitForStartMessageElement => this.WebDriver.FindElementByTestElementId("wait-for-start-message");
        public IWebElement CardChooserElement => this.WebDriver.FindElementByTestElementId("card-chooser");
        public IWebElement EstimationOverviewElement => this.WebDriver.FindElementByTestElementId("estimation-overview");

        public CardChooserComponent CardChooser => new CardChooserComponent(this.CardChooserElement);
        public EstimationOverview EstimationOverview => new EstimationOverview(this.EstimationOverviewElement);

        public void InvokeContinueWorkflow() {
            this.WorkflowContinueButton.Click();

            // Insert sleep for AppVeyor and slower CI
            Thread.Sleep(1000);
        }
    }
}
