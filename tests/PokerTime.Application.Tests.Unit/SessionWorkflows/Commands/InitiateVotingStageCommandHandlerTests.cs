// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateVotingStageCommandHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.SessionWorkflows.Commands {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.SessionWorkflows.Commands;
    using Domain.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public sealed class InitiateVotingStageCommandHandlerTests : RetrospectiveWorkflowCommandTestBase {
        [Test]
        public void InitiateVotingStageCommandHandler_InvalidSessionId_ThrowsNotFoundException() {
            // Given
            const string sessionId = "not found surely :)";
            var handler = new InitiateVotingStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock, this.SystemClockMock);
            var request = new InitiateVotingStageCommand { SessionId = sessionId, TimeInMinutes = 10 };

            // When
            TestDelegate action = () => handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task InitiateVotingStageCommandHandler_OnStatusChange_UpdatesRetroStageAndInvokesNotification() {
            // Given
            var handler = new InitiateVotingStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock, this.SystemClockMock);
            var request = new InitiateVotingStageCommand { SessionId = this.SessionId, TimeInMinutes = 10, VotesPerGroup = 6 };

            this.SystemClockMock.CurrentTimeOffset.Returns(DateTimeOffset.UnixEpoch);

            // When
            await handler.Handle(request, CancellationToken.None);

            this.RefreshObject();

            // Then
            Assert.That(this.Retrospective.CurrentStage, Is.EqualTo(SessionStage.Voting));

            await this.SessionStatusUpdateDispatcherMock.Received().DispatchUpdate(Arg.Any<Retrospective>(), CancellationToken.None);
        }
    }
}
