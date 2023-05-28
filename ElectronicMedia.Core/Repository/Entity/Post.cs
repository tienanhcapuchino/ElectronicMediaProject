using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Column("AuthorId")]
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; } = null;
        public DateTime? UpdatedDate { get; set; } = null;
        public virtual PostCategory Category { get; set; }
        public Guid CategoryId { get; set; }
        [Column("SubCategoryId")]
        public Guid? SubCategoryId { get; set; } = Guid.Empty;
        public virtual PostCategory SubCategory { get; set; }
        public double? Rate { get; set; }
        public int Like { get; set; } = 0;
        public int Dislike { get; set; } = 0;
        public string Image { get; set; }
        public PostStatusModel Status { get; set; }
    }
    public enum PostStatusModel : byte
    {
        Pending = 0,
        Approved = 1,
        Published = 2,
        Denied = 3,
    }
}
