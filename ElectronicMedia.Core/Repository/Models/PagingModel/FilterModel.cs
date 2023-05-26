using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class FilterModel
    {
        public int PageIndex { get; set; }
        public string? FilterValue { get; set; }
        public int PageSize { get; set; }
    }
}
