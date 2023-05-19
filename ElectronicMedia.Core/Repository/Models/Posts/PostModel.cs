using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class PostModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; } = Guid.Empty;
    }
}
