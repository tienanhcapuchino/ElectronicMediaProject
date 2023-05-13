using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class PostDetail
    {
        public Guid Id { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
    }
}
