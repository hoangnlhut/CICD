using DemoCICD.Contract.Abstractions.Message;
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
        public record GetProductQuery() : IQuery<IEnumerable<ProductResponse>>;
        public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
    }
}
