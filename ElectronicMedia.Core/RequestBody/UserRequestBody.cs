using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElectronicMedia.Core
{
    public class PageRequestBody
    {
        public int Page { get; set; }
        public int Top { get; set; }
        public int Skip { get; set; }
        public string SearchText { get; set; }
        public List<string> SearchByColumn { get; set; }
        public PageRequestOrderBy OrderBy { get; set; }
        public IEnumerable<PageRequestFilter> Filter { get; set; }
        public IEnumerable<AdditionalFilter> AdditionalFilters { get; set; }

        public void AddAdditionalFilters(IEnumerable<PageRequestFilter> additionalFilter)
        {
            var filter = Filter?.ToList();
            if(filter is null)
            {
                filter = new List<PageRequestFilter>();
            }
            filter.AddRange(additionalFilter);
            Filter = filter;
        }
    }
}
