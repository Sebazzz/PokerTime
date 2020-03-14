// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetParticipantQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Queries.GetParticipant {
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Abstractions;
    using GetParticipantsInfo;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public sealed class GetParticipantQueryHandler : IRequestHandler<GetParticipantQuery, ParticipantInfo?> {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly IMapper _mapper;

        public GetParticipantQueryHandler(IPokerTimeDbContext pokerTimeDbContext, IMapper mapper) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._mapper = mapper;
        }

        public async Task<ParticipantInfo?> Handle(GetParticipantQuery request, CancellationToken cancellationToken) {
            ParticipantInfo? result = await this._pokerTimeDbContext.Participants.
                    Where(x => x.Session.UrlId.StringId == request.SessionId && x.Name == request.Name).
                    ProjectTo<ParticipantInfo>(this._mapper.ConfigurationProvider).
                    FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
