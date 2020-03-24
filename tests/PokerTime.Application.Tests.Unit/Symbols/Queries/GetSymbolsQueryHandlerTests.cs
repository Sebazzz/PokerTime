// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSymbolsQueryHandlerTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Symbols.Queries {
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Symbols.Queries;
    using NUnit.Framework;
    using Support;

    [TestFixture]
    public sealed class GetSymbolsQueryHandlerTests : QueryTestBase {
        [Test]
        public async Task GetSymbolsQueryHandlerTest_ReturnsSymbols() {
            // Given
            int symbolSetId = this.Context.SymbolSets.First().Id;
            var query = new GetSymbolsQuery(symbolSetId);
            var handler = new GetSymbolsQueryHandler(this.Context, this.Mapper);

            // When
            var result = await handler.Handle(query, CancellationToken.None);

            // Then
            Assert.That(result.Symbols.Select(x => x.Id), Is.EquivalentTo(this.Context.Symbols.Where(x => x.SymbolSetId == symbolSetId).Select(x => x.Id)));
        }
    }
}
