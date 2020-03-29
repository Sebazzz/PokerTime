// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsQueryHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Estimations.Queries {
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Estimations.Queries;
    using Domain.Entities;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class GetEstimationsQueryHandlerTests : QueryTestBase {
        [Test]
        public void GetEstimationsQueryHandlerTests_ThrowsNotFoundException_WhenSessionNotFound() {
            // Given
            const string sessionId = "surely-not-found";
            var query = new GetEstimationsQuery(sessionId, 3);
            var handler = new GetEstimationsQueryHandler(this.Context, this.Mapper);

            // When
            TestDelegate action = () => handler.Handle(query, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task GetEstimationsQueryHandlerTests_ThrowsNotFoundException_WhenUserStoryNotFound() {
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

            var query = new GetEstimationsQuery(sessionId, -1);
            var handler = new GetEstimationsQueryHandler(this.Context, this.Mapper);

            // When
            TestDelegate action = () => handler.Handle(query, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task GetEstimationsQueryHandlerTests_ReturnsUserStoryEstimations() {
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

            this.Context.UserStories.Add(new UserStory {
                Title = "First",
                Estimations =
                {
                    new Estimation {Participant = session.Participants.First(), Symbol = this.Context.Symbols.First()},
                    new Estimation {Participant = session.Participants.Last(), Symbol = this.Context.Symbols.Skip(1).First()}
                },
                Session = session
            });

            UserStory lastUserStory = this.Context.UserStories.Add(new UserStory {
                Title = "First",
                Estimations =
                {
                    new Estimation
                        {Participant = session.Participants.First(), Symbol = this.Context.Symbols.Skip(1).First()},
                    new Estimation {Participant = session.Participants.Last(), Symbol = this.Context.Symbols.First()}
                },
                Session = session
            }).Entity;
            await this.Context.SaveChangesAsync(CancellationToken.None);

            var query = new GetEstimationsQuery(sessionId, lastUserStory.Id);
            var handler = new GetEstimationsQueryHandler(this.Context, this.Mapper);

            // When
            GetEstimationsQueryResponse result = await handler.Handle(query, CancellationToken.None);

            // Then
            Assert.That(result, Is.Not.Null);

            Assert.That(result.Estimations, Has.Count.EqualTo(2));
        }

    }
}
