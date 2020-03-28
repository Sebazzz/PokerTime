// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ServiceCollectionExtensions.cs
//  Project         : PokerTime.Persistence
// ******************************************************************************

namespace PokerTime.Persistence {
    using Application.Common.Abstractions;
    using Common;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddPersistence(this IServiceCollection services) {
            services.AddEntityFrameworkSqlServer();

            services.AddScoped(svc => new PokerTimeDbContext(svc.GetRequiredService<IDatabaseOptions>()));
            services.ChainInterfaceImplementation<IPokerTimeDbContext, PokerTimeDbContext>();
            services.ChainInterfaceImplementation<IEntityStateFacilitator, PokerTimeDbContext>();
            services.ChainInterfaceImplementation<IPokerTimeDbContextFactory, PokerTimeDbContext>();

            services.AddHealthChecks().AddDbContextCheck<PokerTimeDbContext>();

            return services;
        }
    }
}
