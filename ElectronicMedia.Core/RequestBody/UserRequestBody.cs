/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

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
