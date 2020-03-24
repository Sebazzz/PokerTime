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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    public static class ScopeActionsExtensions {
        public static async Task<string> CreatePokerSession(this IServiceScope scope, string facilitatorPassphrase) {
            scope.SetNoAuthenticationInfo();

            var command = new CreatePokerSessionCommand {
                Title = TestContext.CurrentContext.Test.FullName,
                FacilitatorPassphrase = facilitatorPassphrase,
                SymbolSetId = (await scope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>().SymbolSets.FirstAsync()).Id
            };

            CreatePokerSessionCommandResponse result = await scope.Send(command);

            return result.Identifier.StringId;
        }

        public static async Task SetSession(this IServiceScope scope, string sessionId, Action<Session> action) {
            var dbContext = scope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();

            Session session = await dbContext.Sessions.FindBySessionId(sessionId, CancellationToken.None);
            action.Invoke(session);
            await dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public static async Task SetCurrentUserStory(this IServiceScope scope, string sessionId, Action<UserStory> action = null) {
            var dbContext = scope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();

            Session session = await dbContext.Sessions.FindBySessionId(sessionId, CancellationToken.None);
            var userStory = new UserStory { Session = session };
            action?.Invoke(userStory);
            dbContext.UserStories.Add(userStory);

            await dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public static TestCaseBuilder TestCaseBuilder(this IServiceScope scope, string sessionId) => new TestCaseBuilder(scope, sessionId);
    }
}
