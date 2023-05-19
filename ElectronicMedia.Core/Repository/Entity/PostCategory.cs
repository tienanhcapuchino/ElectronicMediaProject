using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class PostCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual PostCategory? Category { get; set; }
        public Guid? ParentId { get; set; }
        public virtual ICollection<PostCategory> SubCategories { get; set; }
    }
}
