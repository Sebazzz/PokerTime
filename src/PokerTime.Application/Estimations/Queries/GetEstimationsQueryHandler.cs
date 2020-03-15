// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
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

    public sealed class GetEstimationsQueryHandler : IRequestHandler<GetEstimationsQuery, GetEstimationsQueryResponse> {
        private readonly IPokerTimeDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetEstimationsQueryHandler(IPokerTimeDbContextFactory dbContextFactory, IMapper mapper) {
            this._dbContextFactory = dbContextFactory;
            this._mapper = mapper;
        }

        public async Task<GetEstimationsQueryResponse> Handle(GetEstimationsQuery request, CancellationToken cancellationToken) {
            using IPokerTimeDbContext dbContext = this._dbContextFactory.CreateForEditContext();

            UserStory userStory = await dbContext.UserStories.
                Where(x => x.Session.UrlId.StringId == request.SessionId).
                Include(x => x.Estimations).
                Include(x => x.Estimations.Select(x => x.Participant)).
                Include(x => x.Estimations.Select(x => x.Symbol)).
                FirstOrDefaultAsync(cancellationToken);

            var response = new GetEstimationsQueryResponse(
                this._mapper.Map<List<EstimationModel>>(userStory.Estimations)
            );

            return response;
        }
    }
}
