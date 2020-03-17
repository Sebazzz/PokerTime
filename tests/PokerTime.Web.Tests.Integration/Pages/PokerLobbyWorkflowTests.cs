﻿// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PokerLobbyWorkflowTests.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Pages {
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Common;
    using Domain.Entities;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using OpenQA.Selenium;

    [TestFixture]
    public class PokerLobbyWorkflowTests : PokerSessionLobbyTestsBase {
        [SetUp]
        public async Task SetUp() {
            using IServiceScope scope = this.App.CreateTestServiceScope();
            this.SessionId = await scope.CreatePokerSession("scrummaster");
        }

        [Test]
        public async Task PokerLobby_ShowsPlainBoard_OnJoiningNewSession() {
            // Given
            await Task.WhenAll(
                Task.Run(() => this.Join(this.Client1, true)),
                Task.Run(() => this.Join(this.Client2, false))
            );

            // When
            this.WaitNavigatedToLobby();

            // Then
            this.MultiAssert(client => Assert.That(() => client.WaitForStartMessageElement, Has.Property(nameof(IWebElement.Displayed)).EqualTo(true).Retry()));
            this.MultiAssert(client => Assert.That(() => client.WebDriver.FindElementsByTestElementId("add-note-button"), Has.Count.EqualTo(0).Retry()));
        }

        [Test]
        public async Task PokerLobby_ShowsCards_OnAdvancingSession() {
            // Given
            await Task.WhenAll(
                Task.Run(() => this.Join(this.Client1, true)),
                Task.Run(() => this.Join(this.Client2, false))
            );
            this.WaitNavigatedToLobby();

            // When
            this.Client1.WorkflowContinueButton.Click();

            // Then
            IPokerTimeDbContext dbContext = this.ServiceScope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();
            var dbCardIds = dbContext.Symbols.Select(x => x.Id).ToList();

            this.MultiAssert(client => {
                Assert.That(() => client.CardChooserElement, Has.Property(nameof(IWebElement.Displayed)).EqualTo(true).Retry(),
                    "The card chooser is not displayed");

                Assert.That(() => {
                    return client.CardChooser.Cards.Select(x => x.Id);
                }, Is.EquivalentTo(dbCardIds).And.Not.Empty.Retry(), "Not all poker cards are shown");

                Assert.That(() => client.CardChooser.Cards.All(x => x.IsChoosable == false),
                    Is.True.Retry(), "Some cards are interactable in this stage, which shouldn't be the case");
            });
        }

        [Test]
        public async Task PokerLobby_ShowsInteractableCards_OnEstimationSession() {
            await this.SetCurrentUserStory();
            await this.SetRetrospective(s => s.CurrentStage = SessionStage.Discussion);

            // Given
            await Task.WhenAll(
                Task.Run(() => this.Join(this.Client1, true)),
                Task.Run(() => this.Join(this.Client2, false))
            );
            this.WaitNavigatedToLobby();

            // When
            this.Client1.InvokeContinueWorkflow();

            // Then
            IPokerTimeDbContext dbContext = this.ServiceScope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();
            var dbCardIds = dbContext.Symbols.Select(x => x.Id).ToList();

            this.MultiAssert(client => {
                Assert.That(() => client.CardChooserElement, Has.Property(nameof(IWebElement.Displayed)).EqualTo(true).Retry(),
                    "The card chooser is not displayed");

                Assert.That(() => client.EstimationOverviewElement, Has.Property(nameof(IWebElement.Displayed)).EqualTo(true).Retry(),
                    "The estimation overview is not displayed");

                Assert.That(() => {
                    return client.CardChooser.Cards.Select(x => x.Id);
                }, Is.EquivalentTo(dbCardIds).And.Not.Empty.Retry(), "Not all poker cards are shown");

                Assert.That(() => client.CardChooser.Cards.All(x => x.IsChoosable == true),
                    Is.True.Retry(), "Some cards are not interactable in this stage, all should be interactable");
            });
        }

        [Test]
        public async Task PokerLobby_CardEstimation_UpdatesOtherView() {
            await this.SetCurrentUserStory();
            await this.SetRetrospective(s => s.CurrentStage = SessionStage.Estimation);

            // Given
            await Task.WhenAll(
                Task.Run(() => this.Join(this.Client1, true)),
                Task.Run(() => this.Join(this.Client2, false))
            );

            this.WaitNavigatedToLobby();

            this.MultiAssert(client => {
                Assume.That(() => client.EstimationOverviewElement, Has.Property(nameof(IWebElement.Displayed)).EqualTo(true).Retry(),
                    "The estimation overview is not displayed");
            });

            // When
            var cards = this.Client2.CardChooser.Cards.ToList();
            var randomCard = cards[TestContext.CurrentContext.Random.Next(cards.Count)];

            var cardId = randomCard.Id;
            TestContext.Write($"Choosing card with symbol #{cardId}");
            randomCard.Click();

            // Then
            this.MultiAssert(client => {
                Assert.That(() => client.EstimationOverview.Cards.Select(c => c.SymbolId), Is.EquivalentTo(new[] { cardId }),
                    $"Expected the card with ID '{cardId}' to be come visible in the estimation overview");
            });
        }

        [Test]
        public async Task PokerLobby_CardEstimationDiscussion_CardsBecomeNonChoosable() {
            await this.SetCurrentUserStory();
            await this.SetRetrospective(s => s.CurrentStage = SessionStage.Estimation);

            // Given
            await Task.WhenAll(
                Task.Run(() => this.Join(this.Client1, true)),
                Task.Run(() => this.Join(this.Client2, false))
            );

            this.MultiAssert(client => {
                Assume.That(() => client.CardChooserElement, Has.Property(nameof(IWebElement.Displayed)).EqualTo(true).Retry(),
                    "The card chooser is not displayed");
            });

            // When
            this.Client1.WorkflowContinueButton.Click();

            this.MultiAssert(client => {
                Assert.That(() => client.CardChooser.Cards.All(x => x.IsChoosable == false),
                    Is.True.Retry(), "Some cards are interactable in this stage, which shouldn't be the case");
            });
        }
    }
}
