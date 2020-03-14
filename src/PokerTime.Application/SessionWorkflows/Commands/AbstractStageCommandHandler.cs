// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : AbstractStageCommandHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SessionWorkflows.Commands {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Common.Abstractions;
    using Common;
    using Domain.Entities;
    using MediatR;
    using Services;

    public abstract class AbstractStageCommandHandler<TRequest> : IRequestHandler<TRequest> where TRequest : AbstractStageCommand, IRequest {
        private readonly ISessionStatusUpdateDispatcher _retrospectiveStatusUpdateDispatcher;
        private readonly IPokerTimeDbContextFactory _dbContextFactory;

#nullable disable
        protected IPokerTimeDbContext DbContext { get; private set; }
#nullable enable

        protected AbstractStageCommandHandler(IPokerTimeDbContextFactory pokerTimeDbContext, ISessionStatusUpdateDispatcher retrospectiveStatusUpdateDispatcher) {
            this._dbContextFactory = pokerTimeDbContext;
            this._retrospectiveStatusUpdateDispatcher = retrospectiveStatusUpdateDispatcher;
        }

        public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try {
                this.DbContext = this._dbContextFactory.CreateForEditContext();
                Retrospective? retrospective = await this.DbContext.Retrospectives.FindBySessionId(request.SessionId, cancellationToken);

                if (retrospective == null) {
                    throw new NotFoundException();
                }

                return await this.HandleCore(request, retrospective, cancellationToken);
            }
            finally {
                this.DbContext?.Dispose();
            }
        }

        protected abstract Task<Unit> HandleCore(TRequest request, Retrospective retrospective, CancellationToken cancellationToken);

        protected Task DispatchUpdate(Retrospective retrospective, CancellationToken cancellationToken) => this._retrospectiveStatusUpdateDispatcher.DispatchUpdate(retrospective, cancellationToken);
    }
}
