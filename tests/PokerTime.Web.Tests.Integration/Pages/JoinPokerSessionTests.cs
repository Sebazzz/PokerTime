// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : JoinPokerSessionTests.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Pages {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Application.PredefinedParticipantColors.Queries.GetAvailablePredefinedParticipantColors;
    using Application.Sessions.Commands.CreatePokerSession;
    using Application.Sessions.Queries.GetParticipantsInfo;
    using Common;
    using Components;
    using Domain.Entities;
    using Domain.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    [TestFixture]
    public sealed class JoinPokerSessionTests : PageFixture<JoinPokerSessionPage> {
        [Test]
        public void JoinPokerSessionPage_UnknownRetrospective_ShowNotFoundMessage() {
            // Given
            string sessionIdentifier = new SessionIdentifierService().CreateNew().StringId;

            // When
            this.Page.Navigate(this.App, sessionIdentifier);

            // Then
            Assert.That(this.Page.Title.Text, Contains.Substring("not found"));
        }

        [Test]
        public async Task JoinPokerSessionPage_KnownRetrospective_FormShownWithValidation() {
            // Given
            string sessionId = await this.CreatePokerSession("scrummaster", "secret");
            this.Page.Navigate(this.App, sessionId);

            // When
            this.Page.Submit();

            // Then
            string[] messages = new DefaultWait<JoinPokerSessionPage>(this.Page)
                .Until(p => {
                    ReadOnlyCollection<IWebElement> collection = p.GetValidationMessages();
                    if (collection.Count == 0) return null;
                    return collection;
                })
                .Select(el => el.Text)
                .ToArray();

            Assert.That(messages, Has.One.Contain("'Name' must not be empty"));
            Assert.That(messages, Has.One.Contain("This passphrase is not valid. Please try again"));
            Assert.That(messages, Has.One.Contain("Please select a color"));
        }

        [Test]
        public async Task JoinPokerSessionPage_KnownRetrospectiveAlreadyStarted_ShowMessage() {
            // Given
            string sessionId = await this.CreatePokerSession("scrummaster", "secret");
            await this.SetRetrospective(sessionId, retro => retro.CurrentStage = SessionStage.Discussion);

            // When
            this.Page.Navigate(this.App, sessionId);

            // Then
            Assert.That(() => this.Page.WebDriver.FindElements(By.CssSelector(".notification.is-info")), Has.Count.EqualTo(1).Retry());
        }

        [Test]
        public async Task JoinPokerSessionPage_KnownRetrospectiveFinished_ShowMessage() {
            // Given
            string sessionId = await this.CreatePokerSession("scrummaster", "secret");
            await this.SetRetrospective(sessionId, retro => retro.CurrentStage = SessionStage.Finished);

            // When
            this.Page.Navigate(this.App, sessionId);

            // Then
            Assert.That(() => this.Page.WebDriver.FindElements(By.CssSelector(".notification.is-warning")), Has.Count.EqualTo(1).Retry());
        }

        [Test]
        public async Task JoinPokerSessionPage_KnownRetrospective_ValidatesParticipantPassphaseAndRedirectsToLobby() {
            // Given
            string sessionId = await this.CreatePokerSession("scrummaster", "secret");
            string myName = Name.Create();
            this.Page.Navigate(this.App, sessionId);

            // When
            this.Page.NameInput.SendKeys(myName);
            new SelectElement(this.Page.ColorSelect).SelectByIndex(1);
            this.Page.ParticipantPassphraseInput.SendKeys("secret");
            this.Page.Submit();

            // Then
            Assert.That(() => this.Page.WebDriver.Url, Does.Match("/pokertime-session/" + sessionId + "/lobby").Retry());
        }

        [Test]
        public async Task JoinPokerSessionPage_KnownRetrospective_JoinParticipantUpdatesParticipantListInRealtime() {
            // Given
            string sessionId = await this.CreatePokerSession("scrummaster", "secret");
            string myName = Name.Create();
            this.Page.Navigate(this.App, sessionId);

            var secondInstance = this.App.CreatePageObject<JoinPokerSessionPage>().RegisterAsTestDisposable();
            secondInstance.Navigate(this.App, sessionId);

            // When
            this.Page.NameInput.SendKeys(myName);
            new SelectElement(this.Page.ColorSelect).SelectByIndex(1);
            this.Page.ParticipantPassphraseInput.SendKeys("secret");
            this.Page.Submit();

            // Then
            Assert.That(() => secondInstance.OnlineList.OnlineListItems.Select(x => x.Text), Has.One.Contains(myName));
        }

        [Test]
        public async Task JoinPokerSessionPage_KnownRetrospective_JoinParticipantUpdatesColorListInRealtime() {
            // Given
            string sessionId = await this.CreatePokerSession("scrummaster", "secret");
            string myName = Name.Create();
            this.Page.Navigate(this.App, sessionId);

            JoinPokerSessionPage secondInstance = this.App.CreatePageObject<JoinPokerSessionPage>().RegisterAsTestDisposable();
            secondInstance.Navigate(this.App, sessionId);

            IList<AvailableParticipantColorModel> availableColors;
            {
                using IServiceScope scope = this.App.CreateTestServiceScope();
                scope.SetNoAuthenticationInfo();
                availableColors = await scope.Send(new GetAvailablePredefinedParticipantColorsQuery(sessionId));
            }
            AvailableParticipantColorModel colorToSelect = availableColors[TestContext.CurrentContext.Random.Next(0, availableColors.Count)];

            // When
            var selectList = new SelectElement(this.Page.ColorSelect);
            Assert.That(() => selectList.Options.Select(x => x.GetProperty("value")).Where(x => !String.IsNullOrEmpty(x)), Is.EquivalentTo(availableColors.Select(x => "#" + x.HexString)).Retry(),
                "Cannot find all available colors in the selection list");
            selectList.SelectByValue("#" + colorToSelect.HexString);

            this.Page.NameInput.SendKeys(myName);
            this.Page.ParticipantPassphraseInput.SendKeys("secret");
            this.Page.Submit();

            // Then
            Assert.That(() => new SelectElement(secondInstance.ColorSelect).Options.Select(x => x.GetAttribute("value")), Does.Not.Contains("#" + colorToSelect.HexString).And.Not.EquivalentTo(availableColors.Select(x => "#" + x.HexString)).Retry());
        }

        [Test]
        public async Task JoinPokerSessionPage_KnownRetrospective_JoinAsFacilitatorUpdatesParticipantListInRealtime() {
            // Given
            string sessionId = await this.CreatePokerSession("scrummaster", "secret");
            string myName = Name.Create();
            this.Page.Navigate(this.App, sessionId);

            var secondInstance = this.App.CreatePageObject<JoinPokerSessionPage>().RegisterAsTestDisposable();
            secondInstance.Navigate(this.App, sessionId);

            // When
            this.Page.NameInput.SendKeys(myName);
            new SelectElement(this.Page.ColorSelect).SelectByIndex(2);
            this.Page.IsFacilitatorCheckbox.Click();
            this.Page.WebDriver.Retry(_ => {
                this.Page.FacilitatorPassphraseInput.SendKeys("scrummaster");
                return true;
            });
            this.Page.Submit();

            Thread.Sleep(500);

            using IServiceScope scope = this.App.CreateTestServiceScope();
            scope.SetNoAuthenticationInfo();
            ParticipantsInfoList participants = await scope.Send(new GetParticipantsInfoQuery(sessionId));
            ParticipantInfo facilitator = participants.Participants.First(x => x.Name == myName);

            // Then
            Assert.That(() => secondInstance.OnlineList.OnlineListItems.Select(x => x.Text), Has.One.Contains(myName));
            Assert.That(() => secondInstance.OnlineList.GetListItem(facilitator.Id).FindElements(By.ClassName("fa-crown")), Is.Not.Empty.Retry());
        }

        private Task SetRetrospective(string sessionId, Action<Session> action) {
            using IServiceScope scope = this.App.CreateTestServiceScope();
            return scope.SetSession(sessionId, action);
        }
        private async Task<string> CreatePokerSession(string facilitatorPassword, string password) {
            var command = new CreatePokerSessionCommand {
                Title = TestContext.CurrentContext.Test.FullName,
                FacilitatorPassphrase = facilitatorPassword,
                Passphrase = password,
                SymbolSetId = (await this.ServiceScope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>().SymbolSets.FirstAsync()).Id
            };

            this.ServiceScope.SetNoAuthenticationInfo();

            CreatePokerSessionCommandResponse result = await this.ServiceScope.Send(command);
            return result.Identifier.StringId;
        }
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Dynamically instantiated")]
    public sealed class JoinPokerSessionPage : PageObject {
        public IWebElement Title => this.WebDriver.FindElement(By.CssSelector("h1.title"));
        public IWebElement NameInput => this.WebDriver.FindElement(By.Id("pokertime-name"));
        public IWebElement FacilitatorPassphraseInput => this.WebDriver.FindElement(By.Id("pokertime-facilitator-passphrase"));
        public IWebElement ParticipantPassphraseInput => this.WebDriver.FindElement(By.Id("pokertime-passphrase"));
        public IWebElement ColorInput => this.WebDriver.FindElement(By.Id("pokertime-color"));
        public IWebElement ColorSelect => this.WebDriver.FindElement(By.Id("pokertime-color-choices"));
        public IWebElement IsFacilitatorCheckbox => this.WebDriver.FindElement(By.Id("pokertime-is-facilitator"));
        public IWebElement SubmitButton => this.WebDriver.FindVisibleElement(By.Id("join-pokertime-button"));
        public void Submit() => this.SubmitButton.Click();

        public ReadOnlyCollection<IWebElement> GetValidationMessages() => this.WebDriver.FindElements(By.ClassName("validation-message"));
        public void Navigate(PokerTimeAppFactory app, string sessionId) => this.WebDriver.NavigateToBlazorPage(app.CreateUri($"pokertime-session/{sessionId}/join"));

        public PokerOnlineListComponent OnlineList => new PokerOnlineListComponent(this.WebDriver);
    }
}
