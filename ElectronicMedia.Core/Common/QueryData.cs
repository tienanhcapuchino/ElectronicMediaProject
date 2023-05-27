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
