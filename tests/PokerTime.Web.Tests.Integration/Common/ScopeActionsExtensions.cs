// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ScopeActionsExtensions.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Common {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Application.Sessions.Commands.CreatePokerSession;
    using Application.Services;
    using Domain.Entities;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    public static class ScopeActionsExtensions {
        public static async Task<string> CreatePokerSession(this IServiceScope scope, string facilitatorPassphrase) {
            scope.SetNoAuthenticationInfo();

            var command = new CreatePokerSessionCommand {
                Title = TestContext.CurrentContext.Test.FullName,
                FacilitatorPassphrase = facilitatorPassphrase
            };

            CreatePokerSessionCommandResponse result = await scope.Send(command);

            return result.Identifier.StringId;
        }

        public static async Task SetRetrospective(this IServiceScope scope, string sessionId, Action<Session> action) {
            var dbContext = scope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();

            Session Session = await dbContext.Sessions.FindBySessionId(sessionId, CancellationToken.None);
            action.Invoke(Session);
            await dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public static TestCaseBuilder TestCaseBuilder(this IServiceScope scope, string retrospectiveId) => new TestCaseBuilder(scope, retrospectiveId);
    }
}
