using AutoMapper;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Application.Mappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Domain.Entities.Product, ProductResponse>().ReverseMap();
        }
    }
}
