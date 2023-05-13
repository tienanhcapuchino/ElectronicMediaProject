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
    public class PostDetailConfiguration : IEntityTypeConfiguration<PostDetail>
    {
        public void Configure(EntityTypeBuilder<PostDetail> builder)
        {
            throw new NotImplementedException();
        }
    }
}
