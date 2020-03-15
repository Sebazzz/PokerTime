// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : PlayCardCommandHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Poker.Commands {
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Abstractions;
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Notifications.EstimationGiven;

    public sealed class PlayCardCommandHandler : IRequestHandler<PlayCardCommand> {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPokerTimeDbContextFactory _dbContextFactory;
        private readonly ICurrentParticipantService _currentParticipantService;

        public PlayCardCommandHandler(IMediator mediator, IPokerTimeDbContextFactory dbContextFactory, ICurrentParticipantService currentParticipantService, IMapper mapper) {
            this._mediator = mediator;
            this._dbContextFactory = dbContextFactory;
            this._currentParticipantService = currentParticipantService;
            this._mapper = mapper;
        }

        public async Task<Unit> Handle(PlayCardCommand request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            using IPokerTimeDbContext dbContext = this._dbContextFactory.CreateForEditContext();

            CurrentParticipantModel currentParticipantInfo = await this._currentParticipantService.GetParticipant();

            Estimation estimation = await dbContext.Estimations
                .Include(x => x.Participant)
                .FirstOrDefaultAsync(x => x.UserStory.Id == request.UserStoryId && x.ParticipantId == currentParticipantInfo.Id, cancellationToken);

            if (estimation == null) {
                estimation = new Estimation {
                    ParticipantId = currentParticipantInfo.Id,
                    Participant = dbContext.Participants.First(x => x.Id == currentParticipantInfo.Id),
                    UserStoryId = request.UserStoryId,
                    Symbol = await dbContext.Symbols.FirstAsync(x => x.Id == request.Symbol.Id, cancellationToken)
                };

                dbContext.Estimations.Add(estimation);
            }
            else {
                estimation.Symbol = await dbContext.Symbols.FirstAsync(x => x.Id == request.Symbol.Id, cancellationToken);
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            var estimationNotification = new EstimationGivenNotification(
                this._mapper.Map<EstimationModel>(estimation)
            );

            await this._mediator.Publish(estimationNotification, cancellationToken);

            return Unit.Value;
        }
    }
}
