using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class Post
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("AuthorId")]
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set;}
        public DateTime UpdatedDate { get; set;}
        public virtual PostCategory Category { get; set; }
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set;}
        public double Rate { get; set; }
    }
}
