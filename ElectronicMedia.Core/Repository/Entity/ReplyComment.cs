using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class ReplyComment
    {
        public Guid Id { get; set; }
        public virtual Comment Comment { get; set; }
        [ForeignKey("ParentId")]
        public Guid ParentId { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
