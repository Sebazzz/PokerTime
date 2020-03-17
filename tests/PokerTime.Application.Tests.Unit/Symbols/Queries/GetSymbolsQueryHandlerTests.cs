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
            var query = new GetSymbolsQuery();
            var handler = new GetSymbolsQueryHandler(this.Context, this.Mapper);

            // When
            var command = new GetSymbolsQuery();

            var result = await handler.Handle(command, CancellationToken.None);

            // Then
            Assert.That(result.Symbols.Select(x => x.Id), Is.EquivalentTo(this.Context.Symbols.Select(x => x.Id)));
        }
    }
}
