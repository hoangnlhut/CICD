using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? _repository.FindAll()
                : _repository.FindAll(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm));

            Expression<Func<Domain.Entities.Product, object>> keySelector = request.SortColumn?.ToLower() switch
            {
                "name" => product => product.Name,
                "price" => product => product.Price,
                "description" => product => product.Description,
                _ => product => product.Name

                // _ => product => product.CreatedDate // Default sort Descending on CreatedDate column
            };

            productsQuery = request.SortOrder == SortOrder.Ascending
                ? productsQuery.OrderBy(keySelector)
                : productsQuery.OrderByDescending(keySelector);

            var products = await productsQuery.ToListAsync(cancellationToken);

            var result = _mapper.Map<List<Response.ProductResponse>>(products);

            return ResultT<List<Response.ProductResponse>>.Success(result);
        }
    }
}
