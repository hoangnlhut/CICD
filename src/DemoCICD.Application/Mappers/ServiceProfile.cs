using AutoMapper;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;

namespace DemoCICD.Application.Mappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Domain.Entities.Product, Response.ProductResponse>().ReverseMap();
            CreateMap<Command.CreateProduct, Domain.Entities.Product>().ReverseMap();
            CreateMap<Command.UpdateProduct, Domain.Entities.Product>().ReverseMap();
            CreateMap<PagedResult<Domain.Entities.Product>, PagedResult<Response.ProductResponse>>().ReverseMap();
        }
    }
}
