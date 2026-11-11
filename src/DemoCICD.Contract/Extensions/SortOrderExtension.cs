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
        public static SortOrder ConvertStringToSortOrder(this string sortOrder)
        {
            //always get the latest data from the database by get descending data
            return !string.IsNullOrWhiteSpace(sortOrder) ? (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase) ? SortOrder.Ascending : SortOrder.Descending) : SortOrder.Descending;
        }
    }
}
