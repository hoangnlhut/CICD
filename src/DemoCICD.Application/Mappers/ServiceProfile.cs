using AutoMapper;
using DemoCICD.Contract.Services.Product;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Application.Mappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Domain.Entities.Product, ProductResponse>().ReverseMap();
            CreateMap<Command.CreateProduct, Domain.Entities.Product>().ReverseMap();
            CreateMap<Command.UpdateProduct, Domain.Entities.Product>().ReverseMap();
        }
    }
}
