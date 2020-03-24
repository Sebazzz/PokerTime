// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSymbolSetsQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SymbolSets.Queries {
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Abstractions;
    using Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public sealed class GetSymbolSetsQueryHandler : IRequestHandler<GetSymbolSetsQuery, GetSymbolSetsQueryResponse> {
        private readonly IPokerTimeDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetSymbolSetsQueryHandler(IPokerTimeDbContextFactory dbContextFactory, IMapper mapper) {
            this._dbContextFactory = dbContextFactory;
            this._mapper = mapper;
        }

        public async Task<GetSymbolSetsQueryResponse> Handle(GetSymbolSetsQuery request, CancellationToken cancellationToken) {
            using IPokerTimeDbContext dbContext = this._dbContextFactory.CreateForEditContext();

            SymbolSetModel[] symbolSets = this._mapper.Map<SymbolSetModel[]>(
                await dbContext.SymbolSets.Include(x => x.Symbols).OrderBy(x => x.Id).ToListAsync(cancellationToken));

            return new GetSymbolSetsQueryResponse(symbolSets);
        }
    }
}
