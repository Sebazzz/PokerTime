﻿// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PokerSessionLobbyTestsBase.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************
namespace PokerTime.Web.Tests.Integration.Pages {
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Common;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using OpenQA.Selenium.Support.UI;

    public class PokerSessionLobbyTestsBase : TwoClientPageFixture<PokerSessionLobby> {
        protected string SessionId { get; set; }
        private int _colorIndex = 1;

        [SetUp]
        public void ResetColorIndex() => this._colorIndex = 1;

        [SuppressMessage("ReSharper", "AccessToDisposedClosure", Justification = "Retry runs while the webdriver runs")]
        protected void Join(PokerSessionLobby pageObject, bool facilitator, string name = null, bool alreadyJoined = false, string colorName = null, Action submitCallback = null) {
            using var joinPage = new JoinPokerSessionPage();
            joinPage.InitializeFrom(pageObject);
            joinPage.Navigate(this.App, this.SessionId);

            joinPage.NameInput.SendKeys(name ?? Name.Create());

            Thread.Sleep(500);
            if (!alreadyJoined) {
                if (colorName != null) {
                    new SelectElement(joinPage.ColorSelect).SelectByText(colorName, true);
                }
                else {
                    new SelectElement(joinPage.ColorSelect).SelectByIndex(this._colorIndex++);
                }
            }
            else {
                // Force refresh
                joinPage.Unfocus();
                Thread.Sleep(500);
            }

            if (facilitator) {
                if (!alreadyJoined) {
                    joinPage.IsFacilitatorCheckbox.Click();
                    Thread.Sleep(500);
                }

                joinPage.WebDriver.Retry(_ => {
                    joinPage.FacilitatorPassphraseInput.SendKeys("scrummaster");
                    return true;
                });
            }

            submitCallback?.Invoke();
            Thread.Sleep(500);
            joinPage.Submit();
            Thread.Sleep(500);
        }

        protected void WaitNavigatedToLobby() =>
            Task.WaitAll(
                Task.Run(() => WaitNavigatedToLobby(this.Client1)),
                Task.Run(() => WaitNavigatedToLobby(this.Client2))
            );

        protected Task SetRetrospective(Action<Session> action) {
            using IServiceScope scope = this.App.CreateTestServiceScope();
            return scope.SetRetrospective(this.SessionId, action);
        }

        protected Task SetCurrentUserStory(Action<UserStory> action = null) {
            using IServiceScope scope = this.App.CreateTestServiceScope();
            return scope.SetCurrentUserStory(this.SessionId, action);
        }

        protected static void WaitNavigatedToLobby(PokerSessionLobby pageObject) {
            var sw = new Stopwatch();
            sw.Start();

            Assume.That(() => pageObject.WebDriver.Url, Does.Match("/lobby").Retry(), "We didn't navigate to the lobby");
            Assume.That(() => pageObject.WebDriver.Retry(wd => wd.FindElementByTestElementId("main-board").Displayed), Is.True.Retry(count: 2), "The retrospective board does not load");

            sw.Stop();
            TestContext.WriteLine($"Navigated to lobby in {sw.Elapsed}");
        }
    }
}
