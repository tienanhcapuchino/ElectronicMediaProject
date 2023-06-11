using ElectronicMedia.Core.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Confiugration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Role);
            builder.Property(x => x.Dob).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(250);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Image);
            builder.Property(x => x.IsActived);
            builder.Property(x => x.Gender).IsRequired().HasDefaultValue(Gender.Unknown);
        }
    }
}
