// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : MappingTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit {
    using System.Drawing;
    using Application.PredefinedParticipantColors.Queries.GetAvailablePredefinedParticipantColors;
    using Application.Sessions.Queries.GetParticipantsInfo;
    using Domain.Entities;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class MappingTests : MappingTestBase {
        [Test]
        public void ShouldHaveValidConfiguration() {
            this.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMap_PredefinedParticipantColor_ToAvailableParticipantColor() {
            // Given
            var entity = new Domain.Entities.PredefinedParticipantColor("Color A", Color.Tomato);

            // When
            var mapped = this.Mapper.Map<AvailableParticipantColorModel>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Name, Is.EqualTo(entity.Name));
            Assert.That(mapped.B, Is.EqualTo(entity.Color.B));
        }

        [Test]
        public void ShouldMap_Participant_ToPredefinedParticipantInfo() {
            // Given
            var entity = new Participant {
                Name = "Josh",
                Color = Color.BlueViolet
            };

            // When
            var mapped = this.Mapper.Map<ParticipantInfo>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Name, Is.EqualTo(entity.Name));
            Assert.That(mapped.Color.R, Is.EqualTo(entity.Color.R));
            Assert.That(mapped.Color.B, Is.EqualTo(entity.Color.B));
            Assert.That(mapped.Color.G, Is.EqualTo(entity.Color.G));
        }
    }
}
