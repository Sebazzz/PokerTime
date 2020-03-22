// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CreatePokerSessionTests.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Pages {
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Common;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    [TestFixture]
    public class CreatePokerSessionTests : PageFixture<CreatePokerSessionPage> {
        [Test]
        public void CreatePokerSession_SubmitWithoutValidation_ShowsValidationMessages() {
            // Given
            this.Page.Navigate(this.App);

            // When
            this.Page.Submit();

            // Then
            string[] messages = new DefaultWait<CreatePokerSessionPage>(this.Page)
                .Until(p => {
                    ReadOnlyCollection<IWebElement> collection = p.GetValidationMessages();
                    if (collection.Count == 0) return null;
                    return collection;
                })
                .Select(el => el.Text)
                .ToArray();

            Assert.That(messages, Has.One.Contain("'Title' must not be empty"));
            Assert.That(messages, Has.One.Contain("'Facilitator Passphrase' must not be empty"));
        }

        [Test]
        public void CreatePokerSession_SubmitValidWithBothPassphrases_ShowQrCodeAndLink() {
            // Given
            this.Page.Navigate(this.App);

            // When
            this.Page.SessionTitleInput.SendKeys(TestContext.CurrentContext.Test.FullName);
            this.Page.FacilitatorPassphraseInput.SendKeys("my secret facilitator password");
            this.Page.ParticipantPassphraseInput.SendKeys("the participator password");

            this.Page.Submit();

            // Then
            Assert.That(this.Page.GetUrlShown(), Does.Match(@"http://localhost:\d+/pokertime-session/([A-z0-9]+)/join"));

            Assert.That(this.Page.FacilitatorInstructions.Text, Contains.Substring("my secret facilitator password"));
            Assert.That(this.Page.ParticipatorInstructions.Text, Contains.Substring("the participator password"));
        }

        [Test]
        public void CreatePokerSession_SubmitValidWithOnlyFacilitatorPassphrase_ShowQrCodeAndLink() {
            // Given
            this.Page.Navigate(this.App);

            // When
            this.Page.SessionTitleInput.SendKeys(TestContext.CurrentContext.Test.FullName);
            this.Page.FacilitatorPassphraseInput.SendKeys("my secret facilitator password");

            this.Page.Submit();

            // Then
            Assert.That(this.Page.GetUrlShown(), Does.Match(@"http://localhost:\d+/pokertime-session/([A-z0-9]+)/join"));

            Assert.That(this.Page.FacilitatorInstructions.Text, Contains.Substring("my secret facilitator password"));
            Assert.That(this.Page.ParticipatorInstructions.Text, Contains.Substring("no password is required"));
        }


    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Dynamically instantiated")]
    public sealed class CreatePokerSessionPage : PageObject {
        public IWebElement SessionTitleInput => this.WebDriver.FindVisibleElement(By.Id("pokertime-title"));
        public IWebElement FacilitatorPassphraseInput => this.WebDriver.FindVisibleElement(By.Id("pokertime-facilitator-passphrase"));
        public IWebElement ParticipantPassphraseInput => this.WebDriver.FindVisibleElement(By.Id("pokertime-passphrase"));
        public IWebElement SubmitButton => this.WebDriver.FindVisibleElement(By.Id("create-pokertime-button"));
        public IWebElement ModalSubmitButton => this.WebDriver.FindVisibleElement(By.Id("modal-create-pokertime-button"));

        public IWebElement UrlLocationInput => this.WebDriver.FindVisibleElement(By.Id("pokertime-location"));
        public IWebElement ParticipatorInstructions => this.WebDriver.FindElementByTestElementId("participator instructions");
        public IWebElement FacilitatorInstructions => this.WebDriver.FindElementByTestElementId("facilitator instructions");

        public IWebElement LobbyCreationPassphraseInput => this.WebDriver.FindVisibleElement(By.Id("pokertime-lobby-creation-passphrase"));
        public IWebElement LobbyCreationPassphraseModal => this.WebDriver.FindElementByTestElementId("lobby-creation-passphrase-modal");

        public bool LobbyCreationPassphraseModalIsDisplayed => this.WebDriver.FindElement(By.CssSelector("[data-test-element-id=\"lobby-creation-passphrase-modal\"]")).Displayed;

        public void Navigate(PokerTimeAppFactory app) => this.WebDriver.NavigateToBlazorPage(app.CreateUri("create-poker-session"));
        public void Submit() => this.SubmitButton.Click();
        public void ModalSubmit() => this.ModalSubmitButton.Click();

        public string GetUrlShown() => this.WebDriver.Retry(_ => this.UrlLocationInput.GetAttribute("value"));
        public ReadOnlyCollection<IWebElement> GetValidationMessages() => this.WebDriver.FindElements(By.ClassName("validation-message"));
    }
}
