// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : EstimationConfiguration.cs
//  Project         : PokerTime.Persistence
// ******************************************************************************

namespace PokerTime.Persistence.Configurations {
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class EstimationConfiguration : IEntityTypeConfiguration<Estimation> {
        public void Configure(EntityTypeBuilder<Estimation> builder) {
            builder.HasOne(x => x.Participant)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Symbol)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
