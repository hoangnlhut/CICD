using DemoCICD.Contract.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Contract.Extensions
{
    public static class SortOrderExtension
    {
        public static SortOrder ConvertStringToSortOrder(string sortOrder)
        {
            //always get the latest data from the database by get descending data
            return !string.IsNullOrWhiteSpace(sortOrder) ? (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase) ? SortOrder.Ascending : SortOrder.Descending) : SortOrder.Descending;
        }

        // Format: Column1-ASC,Column2-DESC...
        public static IDictionary<string, SortOrder> ConvertStringToSortOrderV2(string? sortColumnAndOrder)
        {
            var result = new Dictionary<string, SortOrder>();

            if (!string.IsNullOrEmpty(sortColumnAndOrder))
            {
                if (sortColumnAndOrder.Trim().Split(",").Length > 0)
                {
                    foreach (var item in sortColumnAndOrder.Split(","))
                    {
                        if (!item.Contains('-'))
                            throw new FormatException("Sort condition should be follow by format: Column1-ASC,Column2-DESC...");
                        var property = item.Trim().Split("-");
                        var key = ProductExtension.GetSortProductProperty(property[0]);
                        var value = ConvertStringToSortOrder(property[1]);
                        result.TryAdd(key, value);
                    }
                }
                else
                {
                    if (!sortColumnAndOrder.Contains('-'))
                        throw new FormatException("Sort condition should be follow by format: Column1-ASC,Column2-DESC...");

                    var property = sortColumnAndOrder.Trim().Split("-");
                    result.Add(ProductExtension.GetSortProductProperty(property[0]), ConvertStringToSortOrder(property[1]));
                }
            }

            return result;
        }
    }
}
