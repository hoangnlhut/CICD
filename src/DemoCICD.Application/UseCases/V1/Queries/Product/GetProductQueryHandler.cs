using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Repositories;

namespace DemoCICD.Application.UseCases.V1.Queries.Product
{
    public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, List<Response.ProductResponse>>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultT<List<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
        {

            var products = await _repository.FindAll().Select(p => _mapper.Map<Response.ProductResponse>(p)).ToListAsync();
            return ResultT<List<Response.ProductResponse>>.Success(products);
        }
    }
}
