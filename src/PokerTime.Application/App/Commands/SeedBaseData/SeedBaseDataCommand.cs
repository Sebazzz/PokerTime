// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SeedBaseDataCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.App.Commands.SeedBaseData {
    using Common.Abstractions;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public sealed class SeedBaseDataCommand : IRequest {
    }

    public sealed class SeedBaseDataCommandHandler : IRequestHandler<SeedBaseDataCommand> {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;

        public SeedBaseDataCommandHandler(IPokerTimeDbContext pokerTimeDbContext) {
            this._pokerTimeDbContext = pokerTimeDbContext;
        }

        public async Task<Unit> Handle(SeedBaseDataCommand request, CancellationToken cancellationToken) {
            var seeder = new BaseDataSeeder(this._pokerTimeDbContext);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
