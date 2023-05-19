using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class PostDetailModel
    {
        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
        public bool Liked { get; set; }
    }
}
