// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : FakeDbContext.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Support {
    using System;
    using System.Threading;
    using App.Commands.SeedBaseData;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    public static class ReturnDbContextFactory {
        public static PokerTimeDbContext Create() {
            DbContextOptions<PokerTimeDbContext> options = new DbContextOptionsBuilder<PokerTimeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new PokerTimeDbContext(options);

            context.Database.EnsureCreated();

            new SeedBaseDataCommandHandler(context).Handle(new SeedBaseDataCommand(), CancellationToken.None).
                ConfigureAwait(false).
                GetAwaiter().
                GetResult();

            context.SaveChanges();

            return context;
        }

        public static void Destroy(PokerTimeDbContext context) {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
