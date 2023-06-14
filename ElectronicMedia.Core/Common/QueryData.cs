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
 * Copy right 2023 - PRN231 - SU23 - Group 10. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core
{
    public static class QueryData<T>
    {
        public static IEnumerable<T> QueryForModel(PageRequestBody pageRequest, List<T> values)
        {
            var result = values;
            if (!string.IsNullOrEmpty(pageRequest.OrderBy.OrderByKeyWord))
            {
                if (pageRequest.OrderBy.OrderByDesc)
                {
                    result = values.OrderByDescending(x => pageRequest.OrderBy.OrderByKeyWord).ToList();
                }
                else
                {
                    result = values.OrderBy(x => pageRequest.OrderBy.OrderByKeyWord).ToList();
                }
            }
            if (pageRequest.SearchByColumn.Count > 0)
            {
                result = values.Where(item =>
                pageRequest.SearchByColumn.Any(column => item.GetType()
                .GetProperty(column)?
                .GetValue(item)?
                .ToString()?
                .Contains(pageRequest.SearchText) == true))
                .ToList();
            }
            if(pageRequest.Filter.Count() > 0)
            {
                foreach (var filter in pageRequest.Filter)
                {
                    if (filter.IsNullValue)
                    {
                        result = result.Where(item => filter.IncludeNullValue || filter.Value.Contains(null)).ToList();
                    }
                    else
                    {
                        result = result.Where(item => filter.Value.Contains(item.GetType().GetProperty(filter.ColumnName)?.GetValue(item)?.ToString())).ToList();
                    }
                }
            }
            return result;
        }
    }
}
