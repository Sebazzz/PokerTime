namespace PokerTime.Application.Retrospectives.Queries.GetJoinPokerSessionInfo {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Abstractions;
    using Domain.Entities;
    using Domain.Services;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Services;

    public sealed class GetJoinPokerSessionInfoQueryHandler : IRequestHandler<GetJoinPokerSessionInfoQuery, JoinPokerSessionInfo?> {
        private readonly IPokerTimeDbContext _dbContext;
        private readonly ILogger<GetJoinPokerSessionInfoQueryHandler> _logger;

        public GetJoinPokerSessionInfoQueryHandler(IPokerTimeDbContext dbContext, ILogger<GetJoinPokerSessionInfoQueryHandler> logger) {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<JoinPokerSessionInfo?> Handle(GetJoinPokerSessionInfoQuery request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Retrospective retrospective = await this._dbContext.Retrospectives.FindBySessionId(request.SessionId, cancellationToken);
            if (retrospective == null) {
                this._logger.LogWarning($"Retrospective with id {request.SessionId} was not found");

                return null;
            }

            this._logger.LogInformation($"Retrospective with id {request.SessionId} was found");
            return new JoinPokerSessionInfo(
                retrospective.Title,
                retrospective.HashedPassphrase != null,
                retrospective.IsStarted(),
                retrospective.CurrentStage == RetrospectiveStage.Finished);
        }
    }
}
