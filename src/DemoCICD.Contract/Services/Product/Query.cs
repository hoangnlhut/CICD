using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Contract.Services.Product
{
    public static class Query
    {
        public record GetProductQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder) : IQuery<List<ProductResponse>>;
        public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
    }
}
