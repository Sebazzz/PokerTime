// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : MappingTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit {
    using System.Drawing;
    using System.Linq;
    using Application.Common.Models;
    using Application.Estimations.Queries;
    using Application.PredefinedParticipantColors.Queries.GetAvailablePredefinedParticipantColors;
    using Application.Sessions.Queries.GetParticipantsInfo;
    using Domain.Entities;
    using Estimations.Queries;
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
            var mapped = this.Mapper.Map<UserStoryModel>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Id, Is.EqualTo(entity.Id));
            Assert.That(mapped.Title, Is.EqualTo(entity.Title));
        }

        [Test]
        public void ShouldMap_UserStory_ToUserStoryEstimationsModel() {
            // Given
            Estimation GetEstimationForSymbol(int symbolId) {
                return new Estimation {
                    Symbol = new Symbol {
                        Id = symbolId,
                        Order = TestContext.CurrentContext.Random.Next()
                    }
                };
            }

            var entity = new UserStory {
                Id = 3,
                Title = "ABCD",
                Estimations =
                {
                    GetEstimationForSymbol(1),
                    GetEstimationForSymbol(2),
                    GetEstimationForSymbol(3),
                    GetEstimationForSymbol(3),
                    GetEstimationForSymbol(3),
                    GetEstimationForSymbol(3),
                    GetEstimationForSymbol(4),
                    GetEstimationForSymbol(4),
                    GetEstimationForSymbol(4),
                    GetEstimationForSymbol(5),
                    GetEstimationForSymbol(5),
                }
            };

            // When
            var mapped = this.Mapper.Map<UserStoryEstimation>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Id, Is.EqualTo(entity.Id));
            Assert.That(mapped.Title, Is.EqualTo(entity.Title));
            Assert.That(mapped.Estimations, Has.Count.EqualTo(entity.Estimations.Count));

            Assert.That(mapped.Estimations.Select(x => x.Symbol.Id).ToArray(), Is.EqualTo(new[] { 3, 3, 3, 3, 4, 4, 4, 5, 5, 1, 2 }));

        }

        [Test]
        [TestCase(SymbolType.Number, 2, "2")]
        [TestCase(SymbolType.Number, 100, "100")]
        [TestCase(SymbolType.Infinite, -1, "∞")]
        [TestCase(SymbolType.Break, -1, "☕")]
        [TestCase(SymbolType.Characters, -1, "?")]
        public void ShouldMap_Symbol_ToSymbolModel(SymbolType symbolType, int value, string expected) {
            // Given
            var entity = new Symbol {
                Id = TestContext.CurrentContext.Random.Next(),
                Type = symbolType,
                ValueAsNumber = value,
                ValueAsString = expected
            };

            // When
            var mapped = this.Mapper.Map<SymbolModel>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Id, Is.EqualTo(entity.Id));
            Assert.That(mapped.AsString, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldMap_SymbolSet_ToSymbolSetModel() {
            // Given
            var entity = new SymbolSet {
                Id = TestContext.CurrentContext.Random.Next(),
                Name = TestContext.CurrentContext.Random.GetString(),
                Symbols =
                {
                    new Symbol {Id = 1, ValueAsString = "XS", Order = 1},
                    new Symbol {Id = 2, ValueAsString = "XL", Order = 4},
                    new Symbol {Id = 3, ValueAsString = "M", Order = 2},
                    new Symbol {Id = 4, ValueAsString = "L", Order = 3},
                }
            };

            // When
            var mapped = this.Mapper.Map<SymbolSetModel>(entity);

            // Then
            Assert.That(mapped, Is.Not.Null);

            Assert.That(mapped.Id, Is.EqualTo(entity.Id));
            Assert.That(mapped.Name, Is.EqualTo(entity.Name));

            Assert.That(mapped.Symbols.Select(x => x.Id), Is.EquivalentTo(entity.Symbols.Select(x => x.Id)));

            Assert.That(mapped.Symbols.Select(x => x.Id).ToArray(), Is.EqualTo(new[] { 1, 3, 4, 2 }), "Expected ordered by 'Order' property");
        }
    }
}
