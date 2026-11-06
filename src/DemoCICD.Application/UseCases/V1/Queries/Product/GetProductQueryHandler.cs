using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Repositories;

namespace DemoCICD.Application.UseCases.V1.Queries.Product
{
    public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, List<Response.ProductResponse>>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _repository;

        public GetProductQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> repository)
        {
            _repository = repository;
        }

        public Task<ResultT<List<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = _repository.FindAll(null, null).Select(p => new Response.ProductResponse(p.Id, p.Name, p.Price, p.Description)).ToList();
            return Task.FromResult(ResultT<List<Response.ProductResponse>>.Success(products));
        }
    }
}
