// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IPokerTimeDbContext.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Abstractions {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IPokerTimeDbContext : IPokerTimeDbContextFactory, IDisposable {
        DbSet<PredefinedParticipantColor> PredefinedParticipantColors { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<Participant> Participants { get; set; }
        DbSet<Estimation> Estimations { get; set; }
        DbSet<Symbol> Symbols { get; set; }
        DbSet<SymbolSet> SymbolSets { get; set; }
        DbSet<UserStory> UserStories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
