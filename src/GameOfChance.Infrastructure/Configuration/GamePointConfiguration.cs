using GameOfChance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOfChance.Infrastructure.Configuration
{
    public class GamePointConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.PlayerId).IsRequired();
            builder.Property(c => c.AdditionalBalance).IsRequired();
            builder.Property(c => c.BetPoints).IsRequired();
            builder.Property(c => c.Number).IsRequired();
            builder.Property(c => c.Created).IsRequired();
        }
    }
}
