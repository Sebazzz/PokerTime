// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetJoinPokerSessionInfoQueryHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Sessions.Queries {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Sessions.Queries.GetJoinPokerSessionInfo;
    using Domain.Entities;
    using Microsoft.Extensions.Logging.Abstractions;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class GetJoinPokerSessionInfoQueryHandlerTests : QueryTestBase {
        [Test]
        public async Task GetJoinPokerSessionInfoCommandHandler_ReturnsNull_OnRetrospectiveNotFound() {
            // Given
            string sessionId = "whatever-whatever";
            var handler = new GetJoinPokerSessionInfoQueryHandler(this.Context, new NullLogger<GetJoinPokerSessionInfoQueryHandler>());
            var command = new GetJoinPokerSessionInfoQuery { SessionId = sessionId };

            // When
            var result = await handler.Handle(command, CancellationToken.None);

            // Then
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetJoinPokerSessionInfoCommandHandler_ReturnsInfo_OnRetrospectiveFound() {
            // Given
            var retrospective = new Session {
                Title = "Hello",
                CreationTimestamp = DateTimeOffset.Now,
                HashedPassphrase = "hello"
            };
            string sessionId = retrospective.UrlId.StringId;
            this.Context.Sessions.Add(retrospective);
            await this.Context.SaveChangesAsync(CancellationToken.None);

            var handler = new GetJoinPokerSessionInfoQueryHandler(this.Context, new NullLogger<GetJoinPokerSessionInfoQueryHandler>());
            var command = new GetJoinPokerSessionInfoQuery { SessionId = sessionId };

            // When
            var result = await handler.Handle(command, CancellationToken.None);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Hello"));
            Assert.That(result.IsStarted, Is.False);
            Assert.That(result.IsFinished, Is.False);
            Assert.That(result.NeedsParticipantPassphrase, Is.True);
        }
    }
}
