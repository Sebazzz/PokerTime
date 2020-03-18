// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateDiscussionStageCommandHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.SessionWorkflows.Commands {
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.SessionWorkflows.Commands;
    using Domain.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public sealed class InitiateDiscussionStageCommandHandlerTests : SessionWorkflowCommandTestBase {
        [Test]
        public void InitiateDiscussionStageCommandHandler_InvalidSessionId_ThrowsNotFoundException() {
            // Given
            const string sessionId = "not found surely :)";
            var handler = new InitiateDiscussionStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock);
            var request = new InitiateDiscussionStageCommand { SessionId = sessionId };

            // When
            TestDelegate action = () => handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task InitiateDiscussionStageCommandHandler_OnStatusChange_UpdatesStageAndInvokesNotification() {
            // Given
            var handler = new InitiateDiscussionStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock);
            var request = new InitiateDiscussionStageCommand { SessionId = this.SessionId };

            this.SystemClockMock.CurrentTimeOffset.Returns(DateTimeOffset.UnixEpoch);

            // When
            UserStory userStory = this.Context.UserStories.OrderByDescending(x => x.Id).FirstOrDefault();

            await handler.Handle(request, CancellationToken.None);

            this.RefreshObject();

            UserStory newUserStory = this.Context.UserStories.OrderByDescending(x => x.Id).FirstOrDefault();

            // Then
            Assert.That(this.Session.CurrentStage, Is.EqualTo(SessionStage.Discussion));
            Assert.That(newUserStory?.Id, Is.Not.EqualTo(userStory?.Id), "Expected new user story to be assigned");

            await this.SessionStatusUpdateDispatcherMock.Received().DispatchUpdate(Arg.Any<Session>(), CancellationToken.None);
        }

        [Test]
        public async Task InitiateDiscussionStageCommandHandler_OnReestimationStatusChange_UpdatesStageAndInvokesNotification() {
            // Given
            var handler = new InitiateDiscussionStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock);
            var request = new InitiateDiscussionStageCommand { SessionId = this.SessionId, IsReestimation = true };

            this.SystemClockMock.CurrentTimeOffset.Returns(DateTimeOffset.UnixEpoch);

            // When
            UserStory userStory = this.Context.UserStories.OrderByDescending(x => x.Id).FirstOrDefault();

            await handler.Handle(request, CancellationToken.None);

            this.RefreshObject();

            UserStory newUserStory = this.Context.UserStories.OrderByDescending(x => x.Id).FirstOrDefault();

            // Then
            Assert.That(this.Session.CurrentStage, Is.EqualTo(SessionStage.Estimation));
            Assert.That(newUserStory?.Id, Is.Not.EqualTo(userStory?.Id), "Expected new user story to be assigned");

            await this.SessionStatusUpdateDispatcherMock.Received().DispatchUpdate(Arg.Any<Session>(), CancellationToken.None);
        }
    }
}
