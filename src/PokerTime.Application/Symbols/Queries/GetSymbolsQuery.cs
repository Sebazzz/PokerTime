// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSymbolsQuery.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Symbols.Queries {
    using MediatR;

    public sealed class GetSymbolsQuery : IRequest<GetSymbolsQueryResponse> {
        public int SymbolSetId { get; }

        public GetSymbolsQuery(int symbolSetId) {
            this.SymbolSetId = symbolSetId;
        }
    }
}
