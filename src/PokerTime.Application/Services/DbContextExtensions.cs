// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : DbContextExtensions.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Services {
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public static class RetrospectiveQueryExtensions {
        public static Task<Session> FindBySessionId(
            this IQueryable<Session> queryable,
            string sessionIdentifier,
            CancellationToken cancellationToken
        ) {
            if (sessionIdentifier == null) throw new ArgumentNullException(nameof(sessionIdentifier));

            return queryable.FirstOrDefaultAsync(predicate: x => x.UrlId.StringId == sessionIdentifier,
                cancellationToken: cancellationToken);
        }
    }
}
