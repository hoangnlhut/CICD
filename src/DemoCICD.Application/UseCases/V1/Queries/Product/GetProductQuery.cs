using DemoCICD.Application.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Application.UseCases.V1.Queries.Product
{
    public sealed class GetProductQuery : IQuery<GetProductResponse>
    {
        public string Name { get; set; }
    }
}
