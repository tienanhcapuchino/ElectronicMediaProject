using ElectronicMedia.Core.Repository.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicWeb.Models
{
    public class PostVM
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; } = null;
        public DateTime? UpdatedDate { get; set; } = null;
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; } = Guid.Empty;
        public virtual PostCategory SubCategory { get; set; }
        public double? Rate { get; set; }
        public int Like { get; set; } = 0;
        public int Dislike { get; set; } = 0;
        public string? Image { get; set; }
        public PostStatusModel Status { get; set; }
    }
}
