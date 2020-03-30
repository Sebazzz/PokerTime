// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetEstimationsOverviewQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Estimations.Queries {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Common.Abstractions;
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Services;

    public sealed class GetEstimationsOverviewQueryHandler : IRequestHandler<GetEstimationsOverviewQuery, GetEstimationsOverviewQueryResponse> {
        private readonly IPokerTimeDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetEstimationsOverviewQueryHandler(IPokerTimeDbContextFactory dbContextFactory, IMapper mapper) {
            this._dbContextFactory = dbContextFactory;
            this._mapper = mapper;
        }

        public async Task<GetEstimationsOverviewQueryResponse> Handle(GetEstimationsOverviewQuery request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            using IPokerTimeDbContext dbContext = this._dbContextFactory.CreateForEditContext();

            Session session = await dbContext.Sessions.FindBySessionId(request.SessionId, cancellationToken);
            if (session == null) {
                throw new NotFoundException(nameof(Session), request.SessionId);
            }

            List<UserStory> userStories = await dbContext.UserStories.Where(x => x.SessionId == session.Id).
                OrderBy(x => x.Id).
                Include(x => x.Estimations).
                ThenInclude(x => x.Symbol).
                Include(x => x.Estimations).
                ThenInclude(x => x.Participant).
                ToListAsync(cancellationToken);

            return new GetEstimationsOverviewQueryResponse(
                this._mapper.Map<IEnumerable<UserStoryEstimation>>(userStories)
            );
        }
    }
}
