// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : MappingTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit {
    using System.Drawing;
    using Application.Common.Models;
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

        [Test]
        public void ShouldMap_UserStory_ToUserStoryModel() {
            // Given
            var entity = new UserStory {
                Id = 3,
                Title = "ABCD"
            };

            // When
            var mapped = this.Mapper.Map<CurrentUserStoryModel>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Id, Is.EqualTo(entity.Id));
            Assert.That(mapped.Title, Is.EqualTo(entity.Title));
        }

        [Test]
        [TestCase(SymbolType.Number, 2, "2")]
        [TestCase(SymbolType.Number, 100, "100")]
        [TestCase(SymbolType.Infinite, -1, "∞")]
        [TestCase(SymbolType.Break, -1, "☕")]
        [TestCase(SymbolType.Unknown, -1, "?")]
        public void ShouldMap_Symbol_ToSymbolModel(SymbolType symbolType, int value, string expected) {
            // Given
            var entity = new Symbol {
                Id = TestContext.CurrentContext.Random.Next(),
                Type = symbolType,
                ValueAsNumber = value
            };

            // When
            var mapped = this.Mapper.Map<SymbolModel>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Id, Is.EqualTo(entity.Id));
            Assert.That(mapped.AsString, Is.EqualTo(expected));
        }
    }
}
