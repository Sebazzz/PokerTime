// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetAvailablePredefinedParticipantColorsQueryHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.PredefinedParticipantColors.Queries {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.PredefinedParticipantColors.Queries.GetAvailablePredefinedParticipantColors;
    using Domain.Entities;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class GetAvailablePredefinedParticipantColorsQueryHandlerTests : QueryTestBase {
        [Test]
        public async Task GetAvailablePredefinedParticipantColorsTest() {
            // Given
            var pokerSession = new Session {
                CreationTimestamp = DateTimeOffset.UtcNow,
                FacilitatorHashedPassphrase = "xxx",
                Title = "xxx",
                Participants = { new Participant { Name = "John", Color = Color.Gold } }
            };
            Trace.Assert(pokerSession.UrlId.ToString() != null);
            this.Context.Sessions.Add(pokerSession);
            await this.Context.SaveChangesAsync(CancellationToken.None);

            // When
            var command = new GetAvailablePredefinedParticipantColorsQueryHandler(this.Context, this.Mapper);

            IList<AvailableParticipantColorModel> result = await command.Handle(new GetAvailablePredefinedParticipantColorsQuery(pokerSession.UrlId.StringId), CancellationToken.None);

            // Then
            List<int> colors = result.Select(x => Color.FromArgb(255, x.R, x.G, x.B).ToArgb()).ToList();

            Assert.That(colors, Does.Not.Contains(Color.Gold.ToArgb()));
            Assert.That(colors, Does.Contain(Color.Blue.ToArgb()));
        }
    }
}
