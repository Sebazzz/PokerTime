// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RejoinPokerSessionCommandHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Sessions.Commands {
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Abstractions;
    using Application.Common.Models;
    using Application.Sessions.Commands.RejoinPokerSession;
    using Domain.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class RejoinPokerSessionCommandHandlerTests : CommandTestBase {
        private string _retro1Id;
        private string _retro2Id;

        [OneTimeSetUp]
        public async Task OneTimeSetUp() {
            var retro = new Session {
                Title = "What",
                Participants =
                {
                    new Participant {Name = "John", Color = Color.BlueViolet},
                    new Participant {Name = "Jane", Color = Color.Aqua},
                },
                HashedPassphrase = "abef"
            };
            this._retro1Id = retro.UrlId.StringId;
            this.Context.Sessions.Add(retro);
            await this.Context.SaveChangesAsync(CancellationToken.None);

            var retro2 = new Session {
                Title = "Who",
                Participants =
                {
                    new Participant {Name = "Foo", Color = Color.BlueViolet},
                    new Participant {Name = "Baz", Color = Color.Aqua},
                },
                HashedPassphrase = "abef"
            };
            this._retro2Id = retro2.UrlId.StringId;
            this.Context.Sessions.Add(retro2);
            await this.Context.SaveChangesAsync(CancellationToken.None);
        }

        [Test]
        public async Task RejoinPokerSessionCommandHandler_SetsParticipantInfo_IfFound() {
            // Given
            var authService = Substitute.For<ICurrentParticipantService>();
            var handler = new RejoinPokerSessionCommandHandler(this.Context, authService);
            var query = new RejoinPokerSessionCommand(this._retro1Id, /*John*/ 1);

            // When
            await handler.Handle(query, CancellationToken.None);

            // Then
            authService.Received().SetParticipant(Arg.Any<CurrentParticipantModel>());
        }

        [Test]
        public void RejoinPokerSessionCommandHandler_ThrowsNotFoundException_IfNotFound1() {
            // Given
            var authService = Substitute.For<ICurrentParticipantService>();
            var handler = new RejoinPokerSessionCommandHandler(this.Context, authService);
            var query = new RejoinPokerSessionCommand(this._retro2Id, 2 /*Jane*/);

            // When
            TestDelegate action = () => handler.Handle(query, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public void RejoinPokerSessionCommandHandler_IfNotFound2() {
            // Given
            var authService = Substitute.For<ICurrentParticipantService>();
            var handler = new RejoinPokerSessionCommandHandler(this.Context, authService);
            var query = new RejoinPokerSessionCommand(this._retro1Id, 3 /*Baz*/);

            // When
            TestDelegate action = () => handler.Handle(query, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }
    }
}
