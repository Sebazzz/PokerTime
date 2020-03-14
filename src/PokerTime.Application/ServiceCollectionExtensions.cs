// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ServiceCollectionExtensions.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application {
    using System.Reflection;
    using AutoMapper;
    using Common.Behaviours;
    using Common.Security;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Notifications;
    using Sessions.Queries.GetSessionStatus;
    using SessionWorkflows.Common;

    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(opts => opts.AsScoped(), Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            services.AddScoped<ISessionStatusMapper, SessionStatusMapper>();
            services.AddScoped<ISessionStatusUpdateDispatcher, SessionStatusUpdateDispatcher>();
            services.AddScoped<ISecurityValidator, SecurityValidator>();

            services.AddNotificationDispatchers();

            return services;
        }
    }
}
