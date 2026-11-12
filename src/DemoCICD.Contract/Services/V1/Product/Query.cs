using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoCICD.Contract.Services.V1.Product.Response;

namespace DemoCICD.Contract.Services.V1.Product
{
    public static class Query
    {
        public record GetProductsQuery(string? searchTerm, string? sortColumn, string? sortOrder, IDictionary<string, SortOrder>? sortColumnAndOrder, int pageIndex, int pageSize) : IQuery<PagedResult<ProductResponse>>;
        public record GetProductByIdQuery(Guid id) : IQuery<ProductResponse>;
    }
}
