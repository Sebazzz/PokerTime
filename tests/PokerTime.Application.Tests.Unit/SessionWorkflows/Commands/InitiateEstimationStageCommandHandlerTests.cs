// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateEstimationStageCommandHandlerTests.cs
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
    public sealed class InitiateEstimationStageCommandHandlerTests : SessionWorkflowCommandTestBase {
        [Test]
        public void InitiateEstimationStageCommandHandler_InvalidSessionId_ThrowsNotFoundException() {
            // Given
            const string sessionId = "not found surely :)";
            var handler = new InitiateEstimationStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock);
            var request = new InitiateEstimationStageCommand { SessionId = sessionId };

            // When
            TestDelegate action = () => handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task InitiateEstimationStageCommandHandler_OnStatusChange_UpdatesRetroStageAndInvokesNotification() {
            // Given
            var handler = new InitiateEstimationStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock);
            var request = new InitiateEstimationStageCommand { SessionId = this.SessionId };

            this.SystemClockMock.CurrentTimeOffset.Returns(DateTimeOffset.UnixEpoch);

            // When
            await handler.Handle(request, CancellationToken.None);

            this.RefreshObject();

            // Then
            Assert.That(this.Session.CurrentStage, Is.EqualTo(SessionStage.Estimation));

            await this.SessionStatusUpdateDispatcherMock.Received().DispatchUpdate(Arg.Any<Session>(), CancellationToken.None);
        }
    }
}
