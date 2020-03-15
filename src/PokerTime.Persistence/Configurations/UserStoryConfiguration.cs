// ******************************************************************************
//  © 2020 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : UserStoryConfiguration.cs
//  Project         : PokerTime.Persistence
// ******************************************************************************

namespace PokerTime.Persistence.Configurations {
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class UserStoryConfiguration : IEntityTypeConfiguration<UserStory> {
        public void Configure(EntityTypeBuilder<UserStory> builder) {
            builder.HasMany(x => x.Estimations)
                .WithOne(x => x.UserStory)
                .IsRequired()
                .HasForeignKey(x => x.UserStoryId);

            builder.HasOne(x => x.Session)
                .WithMany()
                .HasForeignKey(x => x.SessionId)
                .IsRequired();
        }
    }
}
