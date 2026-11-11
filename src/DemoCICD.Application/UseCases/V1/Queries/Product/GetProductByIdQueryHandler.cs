using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using static DemoCICD.Domain.Exceptions.ProductException;

namespace DemoCICD.Application.UseCases.V1.Queries.Product
{
    public sealed class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductByIdQuery, Response.ProductResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultT<Response.ProductResponse>> Handle(Query.GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindByIdAsync(request.id, cancellationToken, null);
            if (product == null)
            {
                throw new ProductNotFoundException(request.id);
            }

            var result = _mapper.Map<Response.ProductResponse>(product);

            return ResultT<Response.ProductResponse>.Success(result);
        }
    }
}
