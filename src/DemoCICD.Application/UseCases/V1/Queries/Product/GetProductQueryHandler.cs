using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DemoCICD.Contract.Services.V1.Product;
using MediatR;
using System.Globalization;

namespace DemoCICD.Application.UseCases.V1.Queries.Product
{
    public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductsQuery, PagedResult<Response.ProductResponse>>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public GetProductQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> repository, ApplicationDbContext context, IMapper mapper)
        {
            this._productRepository = repository;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ResultT<PagedResult<Response.ProductResponse>>> Handle(Query.GetProductsQuery request, CancellationToken cancellationToken)
        {
            if (request.sortColumnAndOrder.Any()) // =>>  Raw Query when order by multi column
            {
                var PageIndex = request.pageIndex <= 0 ? PagedResult<Domain.Entities.Product>.DefaultPageIndex : request.pageIndex;
                var PageSize = request.pageSize <= 0
                    ? PagedResult<Domain.Entities.Product>.DefaultPageSize
                    : request.pageSize > PagedResult<Domain.Entities.Product>.UpperPageSize
                    ? PagedResult<Domain.Entities.Product>.UpperPageSize : request.pageSize;

                // ============================================
                var productsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
                    ? @$"SELECT * FROM {nameof(Domain.Entities.Product)} ORDER BY "
                    : @$"SELECT * FROM {nameof(Domain.Entities.Product)}
                        WHERE {nameof(Domain.Entities.Product.Name)} LIKE '%{request.searchTerm}%'
                        OR {nameof(Domain.Entities.Product.Description)} LIKE '%{request.searchTerm}%'
                        ORDER BY ";

                foreach (var item in request.sortColumnAndOrder)
                    productsQuery += item.Value == SortOrder.Descending
                        ? $"{item.Key} DESC, "
                        : $"{item.Key} ASC, ";

                productsQuery = productsQuery.Remove(productsQuery.Length - 2);

                productsQuery += $" OFFSET {(PageIndex - 1) * PageSize} ROWS FETCH NEXT {PageSize} ROWS ONLY";

                var products = await _context.Products.FromSqlRaw(productsQuery).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                var totalCount = await _context.Products.CountAsync(cancellationToken);

                var productPagedResult = PagedResult<Domain.Entities.Product>.Create(products,
                    PageIndex,
                    PageSize,
                    totalCount);

                var result = _mapper.Map<PagedResult<Response.ProductResponse>>(productPagedResult);

                return Result.Success(result);
            }
            else // =>> Entity Framework
            {
                var productsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
                ? _productRepository.FindAll()
                : _productRepository.FindAll(x => x.Name.Contains(request.searchTerm) || x.Description.Contains(request.searchTerm));

                productsQuery = request.sortOrder == SortOrder.Descending
                ? productsQuery.OrderByDescending(GetSortProperty(request))
                : productsQuery.OrderBy(GetSortProperty(request));

                var products = await PagedResult<Domain.Entities.Product>.CreateAsync(productsQuery,
                    request.pageIndex,
                    request.pageSize);

                var result = _mapper.Map<PagedResult<Response.ProductResponse>>(products);
                return Result.Success(result);
            }
        }

        private static Expression<Func<Domain.Entities.Product, object>> GetSortProperty(Query.GetProductsQuery request)
             => request.sortColumn?.ToLower(CultureInfo.CurrentCulture) switch
             {
                 "name" => product => product.Name,
                 "price" => product => product.Price,
                 "description" => product => product.Description,
                 _ => product => product.Id

                 // _ => product => product.CreatedDate // Default Sort Descending on CreatedDate column
             };
    }
}
