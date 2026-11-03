using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Shared;
using MediatR;

namespace DemoCICD.Application.UseCases.V1.Queries.Product
{
    public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductResponse>
    {
        public Task<ResultT<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
