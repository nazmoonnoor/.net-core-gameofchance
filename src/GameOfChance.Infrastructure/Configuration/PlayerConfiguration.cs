using GameOfChance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.Infrastructure.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FullName).HasMaxLength(200);
            builder.Property(c => c.Email).HasMaxLength(200).IsRequired();
            builder.Property(c => c.AccountBalance).IsRequired();
            builder.Property(c => c.Created).IsRequired();
        }
    }
}
