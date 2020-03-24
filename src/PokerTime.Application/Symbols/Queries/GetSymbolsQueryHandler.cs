// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSymbolsQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Symbols.Queries {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Abstractions;
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public sealed class GetSymbolsQueryHandler : IRequestHandler<GetSymbolsQuery, GetSymbolsQueryResponse> {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IMapper _mapper;

        public GetSymbolsQueryHandler(IPokerTimeDbContext pokerTimeDbContext, IMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public async Task<GetSymbolsQueryResponse> Handle(GetSymbolsQuery request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            IEnumerable<Symbol> allSymbols = await this._pokerTimeDbContext.Symbols.Where(x => x.SymbolSetId == request.SymbolSetId).OrderBy(x => x.Order).ToListAsync(cancellationToken);

            return new GetSymbolsQueryResponse(this._mapper.Map<List<SymbolModel>>(allSymbols));
        }
    }
}
