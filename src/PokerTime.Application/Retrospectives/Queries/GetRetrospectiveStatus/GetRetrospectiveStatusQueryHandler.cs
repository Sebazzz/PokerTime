// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetRetrospectiveStatusQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetRetrospectiveStatus {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Common.Abstractions;
    using Domain.Entities;
    using MediatR;
    using Services;

    public sealed class GetRetrospectiveStatusQueryHandler : IRequestHandler<GetRetrospectiveStatusQuery, RetrospectiveStatus> {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IRetrospectiveStatusMapper _mapper;

        public GetRetrospectiveStatusQueryHandler(IPokerTimeDbContext pokerTimeDbContext, IRetrospectiveStatusMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public async Task<RetrospectiveStatus> Handle(GetRetrospectiveStatusQuery request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Retrospective retrospective = await this._pokerTimeDbContext.Retrospectives.FindBySessionId(request.SessionId, cancellationToken);

            if (retrospective == null) {
                throw new NotFoundException();
            }

            return await this._mapper.GetRetrospectiveStatus(retrospective, cancellationToken);
        }
    }
}
