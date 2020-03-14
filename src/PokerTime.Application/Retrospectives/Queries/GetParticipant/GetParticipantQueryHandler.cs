﻿// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : GetParticipantQueryHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Retrospectives.Queries.GetParticipant {
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
        private readonly IReturnDbContext _returnDbContext;
        private readonly IMapper _mapper;

        public GetParticipantQueryHandler(IReturnDbContext returnDbContext, IMapper mapper) {
            this._returnDbContext = returnDbContext;
            this._mapper = mapper;
        }

        public async Task<ParticipantInfo?> Handle(GetParticipantQuery request, CancellationToken cancellationToken) {
            ParticipantInfo? result = await this._returnDbContext.Participants.
                    Where(x => x.Retrospective.UrlId.StringId == request.SessionId && x.Name == request.Name).
                    ProjectTo<ParticipantInfo>(this._mapper.ConfigurationProvider).
                    FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}