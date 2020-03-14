// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateVotingStageCommandHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.RetrospectiveWorkflows.Commands {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.RetrospectiveWorkflows.Commands;
    using Domain.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public sealed class InitiateVotingStageCommandHandlerTests : RetrospectiveWorkflowCommandTestBase {
        [Test]
        public void InitiateVotingStageCommandHandler_InvalidSessionId_ThrowsNotFoundException() {
            // Given
            const string sessionId = "not found surely :)";
            var handler = new InitiateVotingStageCommandHandler(this.Context, this.RetrospectiveStatusUpdateDispatcherMock, this.SystemClockMock);
            var request = new InitiateVotingStageCommand { SessionId = sessionId, TimeInMinutes = 10 };

            // When
            TestDelegate action = () => handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task InitiateVotingStageCommandHandler_OnStatusChange_UpdatesRetroStageAndInvokesNotification() {
            // Given
            var handler = new InitiateVotingStageCommandHandler(this.Context, this.RetrospectiveStatusUpdateDispatcherMock, this.SystemClockMock);
            var request = new InitiateVotingStageCommand { SessionId = this.SessionId, TimeInMinutes = 10, VotesPerGroup = 6 };

            this.SystemClockMock.CurrentTimeOffset.Returns(DateTimeOffset.UnixEpoch);

            // When
            await handler.Handle(request, CancellationToken.None);

            this.RefreshObject();

            // Then
            Assert.That(this.Retrospective.CurrentStage, Is.EqualTo(RetrospectiveStage.Voting));

            Assert.That(this.Retrospective.Options.MaximumNumberOfVotes, Is.EqualTo(6));

            Assert.That(this.Retrospective.WorkflowData.CurrentWorkflowInitiationTimestamp, Is.EqualTo(this.SystemClockMock.CurrentTimeOffset));
            Assert.That(this.Retrospective.WorkflowData.CurrentWorkflowTimeLimitInMinutes, Is.EqualTo(request.TimeInMinutes));

            await this.RetrospectiveStatusUpdateDispatcherMock.Received().DispatchUpdate(Arg.Any<Retrospective>(), CancellationToken.None);
        }
    }
}
