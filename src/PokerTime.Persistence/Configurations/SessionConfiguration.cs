// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionConfiguration.cs
//  Project         : PokerTime.Persistence
// ******************************************************************************


namespace PokerTime.Persistence.Configurations {
    using System;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class SessionConfiguration : IEntityTypeConfiguration<Session> {
        public void Configure(EntityTypeBuilder<Session> builder) {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.Id);

            builder.OwnsOne(e => e.UrlId,
                e => {
                    e.WithOwner();
                    e.HasIndex(x => x.StringId).IsUnique();
                    e.Property(x => x.StringId).HasMaxLength(32).IsUnicode(false).IsRequired();
                });

            builder.Property(e => e.Title).HasMaxLength(256);
            builder.Property(e => e.HashedPassphrase).HasMaxLength(64).IsUnicode(false).IsRequired(false).IsFixedLength();
            builder.Property(e => e.FacilitatorHashedPassphrase).IsRequired().HasMaxLength(64).IsUnicode(false).IsFixedLength();

            builder.HasMany(e => e.Participants).
                WithOne(e => e.Session).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.SymbolSet)
                   .WithMany()
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.UsePropertyAccessMode(PropertyAccessMode.PreferProperty);
        }
    }
}

