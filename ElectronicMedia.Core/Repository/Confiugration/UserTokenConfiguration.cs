using ElectronicMedia.Core.Repository.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Confiugration
{
    internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("userToken");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.IssuedAt);
            builder.Property(t => t.ExpiredAt);
            builder.Property(t => t.IsRevoked);
            builder.Property(t => t.AccessToken);
            builder.Property(t => t.RefreshToken);
            builder.Property(t => t.IsUsed);
            builder.Property(t => t.JwtId);
        }
    }
}
