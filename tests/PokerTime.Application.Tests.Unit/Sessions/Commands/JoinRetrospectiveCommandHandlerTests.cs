// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : JoinPokerSessionCommandHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Sessions.Commands {
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Abstractions;
    using Application.Common.Models;
    using Application.Notifications.SessionJoined;
    using Application.Sessions.Commands.JoinPokerSession;
    using AutoMapper;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using NSubstitute;
    using NSubstitute.ReceivedExtensions;
    using NUnit.Framework;
    using Services;
    using Support;

    [TestFixture]
    public sealed class JoinPokerSessionCommandHandlerTests : CommandTestBase {
        private Session _session;

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

            this.Context.Sessions.Add(retro);
            await this.Context.SaveChangesAsync(CancellationToken.None);

            this._session = retro;
        }

        [Test]
        public void JoinPokerSessionCommand_ThrowsException_WhenNotFound() {
            // Given
            var command = new JoinPokerSessionCommand {
                SessionId = "not found"
            };
            var handler = new JoinPokerSessionCommandHandler(this.Context, Substitute.For<ICurrentParticipantService>(), Substitute.For<IMediator>(), Substitute.For<IMapper>());

            // When
            TestDelegate action = () => handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<NotFoundException>());
        }

        [Test]
        public async Task JoinPokerSessionCommand_SetsParticipantId_WhenJoiningRetrospective() {
            // Given
            var retro = this._session ?? throw new InvalidOperationException("OneTimeSetup not executed");

            var mediator = Substitute.For<IMediator>();
            var mapper = Substitute.For<IMapper>();

            var currentParticipantService = Substitute.For<ICurrentParticipantService>();
            var handler = new JoinPokerSessionCommandHandler(
                this.Context,
                currentParticipantService,
                mediator,
                mapper
            );

            var command = new JoinPokerSessionCommand {
                SessionId = retro.UrlId.StringId,
                Color = "ABCDEF",
                JoiningAsFacilitator = true,
                Name = "The BOSS",
                Passphrase = "Not relevant"
            };

            // When
            await handler.Handle(command, CancellationToken.None);

            // Then
            currentParticipantService.ReceivedWithAnyArgs(Quantity.Exactly(1))
                .SetParticipant(Arg.Any<CurrentParticipantModel>());

            Session checkRetro = await this.Context.Sessions.AsNoTracking().
                Include(x => x.Participants).
                FindBySessionId(retro.UrlId.StringId, CancellationToken.None).
                ConfigureAwait(false);

            Assert.That(checkRetro.Participants.Select(x => x.Name), Contains.Item("The BOSS"));

            await mediator.Received().
                Publish(Arg.Any<SessionJoinedNotification>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task JoinPokerSessionCommand_DuplicateJoin_DoesNotCreateNewParticipant() {
            // Given
            var retro = this._session ?? throw new InvalidOperationException("OneTimeSetup not executed");

            var mediator = Substitute.For<IMediator>();
            var mapper = Substitute.For<IMapper>();

            var currentParticipantService = Substitute.For<ICurrentParticipantService>();
            var handler = new JoinPokerSessionCommandHandler(
                this.Context,
                currentParticipantService,
                mediator,
                mapper
            );

            var command = new JoinPokerSessionCommand {
                SessionId = retro.UrlId.StringId,
                Color = "ABCDEF",
                JoiningAsFacilitator = true,
                Name = "Duplicate joiner",
                Passphrase = "Not relevant"
            };

            // When
            await handler.Handle(command, CancellationToken.None);

            await handler.Handle(command, CancellationToken.None);

            // Then
            var participants = await this.Context.Sessions.
                 SelectMany(x => x.Participants).AsNoTracking().ToListAsync();

            Assert.That(participants.Count(x => x.Name == "Duplicate joiner"), Is.EqualTo(1));
        }
    }
}
