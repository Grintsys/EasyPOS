using AutoMapper;
using Grintsys.EasyPOS.CreateUpdateDtos;
using Grintsys.EasyPOS.Dtos;
using Grintsys.EasyPOS.Models;

namespace Grintsys.EasyPOS
{
    public class EasyPOSApplicationAutoMapperProfile : Profile
    {
        public EasyPOSApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateUpdateCustomerDto, Customer>();
        }
    }
}
