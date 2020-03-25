// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSymbolSetsQueryHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.SymbolSets.Queries {
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.SymbolSets.Queries;
    using Domain.Entities;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class GetSymbolSetsQueryHandlerTests : QueryTestBase {
        [Test]
        public async Task GetSymbolSetsQueryHandler_Handle_ReturnsMappedSymbols() {
            // Given
            var handler = new GetSymbolSetsQueryHandler(this.Context, this.Mapper);
            var query = new GetSymbolSetsQuery();

            this.Context.SymbolSets.Add(new SymbolSet {
                Name = "ABC",
                Symbols =
                {
                    new Symbol {Type = SymbolType.Characters, ValueAsString = "XL", Order = 2},
                    new Symbol {Type = SymbolType.Characters, ValueAsString = "XS", Order = 1},
                }
            });

            this.Context.SymbolSets.Add(new SymbolSet {
                Name = "DEF",
                Symbols =
                {
                    new Symbol {Type = SymbolType.Characters, ValueAsString = "10", Order = 2},
                    new Symbol {Type = SymbolType.Characters, ValueAsString = "2", Order = 1},
                }
            });

            await this.Context.SaveChangesAsync(CancellationToken.None);

            // When
            GetSymbolSetsQueryResponse response = await handler.Handle(query, CancellationToken.None);

            // Then
            // Note that two are seeded by default in the test database context as well
            Assert.That(response.SymbolSets.Select(x => x.Name), Is.EquivalentTo(new[] { "Default", "T-shirt sizes", "Fibonacci", "Powers of two", "ABC", "DEF" }));
        }

    }
}
