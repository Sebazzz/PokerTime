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
        DbSet<Retrospective> Retrospectives { get; set; }
        DbSet<Participant> Participants { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
