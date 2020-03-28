// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Sessions.Queries {
    using Application.Common.Models;
    using Application.Sessions.Queries.GetSessionStatus;
    using Domain.Entities;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SessionStatusTests {
        [Test]
        [TestCase(SessionStage.NotStarted, ExpectedResult = false)]
        [TestCase(SessionStage.Discussion, ExpectedResult = true)]
        [TestCase(SessionStage.Estimation, ExpectedResult = true)]
        [TestCase(SessionStage.EstimationDiscussion, ExpectedResult = true)]
        [TestCase(SessionStage.Finished, ExpectedResult = false)]
        public bool SessionStatus_CanViewOwnCards(SessionStage sessionStage) => GetSessionStatusInStage(sessionStage).CanViewOwnCards;

        [Test]
        [TestCase(SessionStage.NotStarted, ExpectedResult = false)]
        [TestCase(SessionStage.Discussion, ExpectedResult = false)]
        [TestCase(SessionStage.Estimation, ExpectedResult = true)]
        [TestCase(SessionStage.EstimationDiscussion, ExpectedResult = false)]
        [TestCase(SessionStage.Finished, ExpectedResult = false)]
        public bool SessionStatus_CanChooseCards(SessionStage sessionStage) => GetSessionStatusInStage(sessionStage).CanChooseCards;

        [Test]
        [TestCase(SessionStage.NotStarted, ExpectedResult = false)]
        [TestCase(SessionStage.Discussion, ExpectedResult = false)]
        [TestCase(SessionStage.Estimation, ExpectedResult = true)]
        [TestCase(SessionStage.EstimationDiscussion, ExpectedResult = true)]
        [TestCase(SessionStage.Finished, ExpectedResult = false)]
        public bool SessionStatus_CanViewEstimationPanel(SessionStage sessionStage) => GetSessionStatusInStage(sessionStage).CanViewEstimationPanel;

        [Test]
        [TestCase(SessionStage.NotStarted, ExpectedResult = false)]
        [TestCase(SessionStage.Discussion, ExpectedResult = false)]
        [TestCase(SessionStage.Estimation, ExpectedResult = false)]
        [TestCase(SessionStage.EstimationDiscussion, ExpectedResult = true)]
        [TestCase(SessionStage.Finished, ExpectedResult = true)]
        public bool SessionStatus_CanViewEstimations(SessionStage sessionStage) => GetSessionStatusInStage(sessionStage).CanViewEstimations;

        [Test]
        [TestCase(SessionStage.NotStarted, ExpectedResult = false)]
        [TestCase(SessionStage.Discussion, ExpectedResult = false)]
        [TestCase(SessionStage.Estimation, ExpectedResult = false)]
        [TestCase(SessionStage.EstimationDiscussion, ExpectedResult = false)]
        [TestCase(SessionStage.Finished, ExpectedResult = true)]
        public bool SessionStatus_ShowUserStoriesOverview(SessionStage sessionStage) => GetSessionStatusInStage(sessionStage).ShowUserStoriesOverview;

        private static SessionStatus GetSessionStatusInStage(SessionStage sessionStage) => new SessionStatus(
            TestContext.CurrentContext.Random.GetString(),
            TestContext.CurrentContext.Random.GetString(),
            sessionStage,
            TestContext.CurrentContext.Random.Next(),
            new UserStoryModel());
    }
}
