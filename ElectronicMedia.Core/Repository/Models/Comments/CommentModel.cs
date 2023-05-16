using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class CommentModel
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
    }
}
