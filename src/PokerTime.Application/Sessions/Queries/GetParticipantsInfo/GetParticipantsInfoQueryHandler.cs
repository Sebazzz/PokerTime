// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetParticipantsInfoQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetParticipantsInfo {
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Abstractions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public sealed class GetParticipantsInfoQueryHandler : IRequestHandler<GetParticipantsInfoQuery, ParticipantsInfoList> {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IMapper _mapper;

        public GetParticipantsInfoQueryHandler(IPokerTimeDbContext pokerTimeDbContext, IMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public async Task<ParticipantsInfoList> Handle(GetParticipantsInfoQuery request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var returnValue = new ParticipantsInfoList();
            returnValue.Participants.AddRange(
                await this._pokerTimeDbContext.Sessions
                    .Where(r => r.UrlId.StringId == request.SessionId)
                    .SelectMany(r => r.Participants)
                    .ProjectTo<ParticipantInfo>(this._mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken)
            );

            return returnValue;
        }
    }
}
