using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class PostCategoryModel
    {
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
