// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateWritingStageCommandHandlerTests.cs
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
    public sealed class InitiateWritingStageCommandHandlerTests : RetrospectiveWorkflowCommandTestBase {
        [Test]
        public void InitiateWritingStageCommandHandler_InvalidSessionId_ThrowsNotFoundException() {
            // Given
            const string sessionId = "not found surely :)";
            var handler = new InitiateWritingStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock, this.SystemClockMock);
            var request = new InitiateWritingStageCommand { SessionId = sessionId, TimeInMinutes = 10 };

            // When
            TestDelegate action = () => handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task InitiateWritingStageCommandHandler_OnStatusChange_UpdatesRetroStageAndInvokesNotification() {
            // Given
            var handler = new InitiateWritingStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock, this.SystemClockMock);
            var request = new InitiateWritingStageCommand { SessionId = this.SessionId, TimeInMinutes = 10 };

            this.SystemClockMock.CurrentTimeOffset.Returns(DateTimeOffset.UnixEpoch);

            // When
            await handler.Handle(request, CancellationToken.None);

            this.RefreshObject();

            // Then
            Assert.That(this.Retrospective.CurrentStage, Is.EqualTo(SessionStage.Writing));

            await this.SessionStatusUpdateDispatcherMock.Received().DispatchUpdate(Arg.Any<Retrospective>(), CancellationToken.None);
        }
    }
}
