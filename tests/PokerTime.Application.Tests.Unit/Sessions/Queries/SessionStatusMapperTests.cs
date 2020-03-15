// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStatusMapperTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Sessions.Queries {
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Sessions.Queries.GetSessionStatus;
    using Domain.Entities;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class SessionStatusMapperTests : QueryTestBase {
        [Test]
        public void SessionStatusMapper_NullArgument_ThrowsArgumentNullException() {
            // Given
            var mapper = new SessionStatusMapper(this.Context,this.Mapper);

            // When
            TestDelegate action = () => mapper.GetSessionStatus(null, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.ArgumentNullException);
        }

        [Test]
        public async Task SessionStatusMapper_ReturnsSessionInfo() {
            // Given
            var session = new Session {
                Title = "Yet another test",
                Participants =
                {
                    new Participant { Name = "John", Color = Color.BlueViolet },
                    new Participant { Name = "Jane", Color = Color.Aqua },
                },
                HashedPassphrase = "abef",
                CurrentStage = SessionStage.Discussion
            };
            string sessionId = session.UrlId.StringId;
            this.Context.Sessions.Add(session);
            await this.Context.SaveChangesAsync(CancellationToken.None);

            var mapper = new SessionStatusMapper(this.Context, this.Mapper);

            // When
            SessionStatus result = await mapper.GetSessionStatus(session, CancellationToken.None);

            // Then
            Assert.That(result.SessionId, Is.EqualTo(sessionId));
            Assert.That(result.Title, Is.EqualTo(session.Title));
        }
    }
}
