﻿// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : InitiateGroupingStageCommandHandlerTests.cs
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
    public sealed class InitiateGroupingStageCommandHandlerTests : RetrospectiveWorkflowCommandTestBase {
        [Test]
        public void InitiateGroupingStageCommandHandler_InvalidSessionId_ThrowsNotFoundException() {
            // Given
            const string sessionId = "not found surely :)";
            var handler = new InitiateGroupingStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock);
            var request = new InitiateGroupingStageCommand { SessionId = sessionId };

            // When
            TestDelegate action = () => handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task InitiateGroupingStageCommandHandler_OnStatusChange_UpdatesRetroStageAndInvokesNotification() {
            // Given
            var handler = new InitiateGroupingStageCommandHandler(this.Context, this.SessionStatusUpdateDispatcherMock);
            var request = new InitiateGroupingStageCommand { SessionId = this.SessionId };

            this.SystemClockMock.CurrentTimeOffset.Returns(DateTimeOffset.UnixEpoch);

            // When
            await handler.Handle(request, CancellationToken.None);

            this.RefreshObject();

            // Then
            Assert.That(this.Session.CurrentStage, Is.EqualTo(SessionStage.Grouping));

            await this.SessionStatusUpdateDispatcherMock.Received().DispatchUpdate(Arg.Any<Session>(), CancellationToken.None);
        }
    }
}