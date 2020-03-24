// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetSessionStatusQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetSessionStatus {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Common.Abstractions;
    using Domain.Entities;
    using MediatR;
    using Services;

    public sealed class GetSessionStatusQueryHandler : IRequestHandler<GetSessionStatusQuery, SessionStatus> {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly ISessionStatusMapper _mapper;

        public GetSessionStatusQueryHandler(IPokerTimeDbContext pokerTimeDbContext, ISessionStatusMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public async Task<SessionStatus> Handle(GetSessionStatusQuery request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Session session = await this._pokerTimeDbContext.Sessions.FindBySessionId(request.SessionId, cancellationToken);

            if (session == null) {
                throw new NotFoundException();
            }

            return await this._mapper.GetSessionStatus(session, cancellationToken);
        }
    }
}
