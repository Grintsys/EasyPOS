using AutoMapper;
using Grintsys.EasyPOS.Customer;
using Grintsys.EasyPOS.Order;
using Grintsys.EasyPOS.OrderItem;
using Grintsys.EasyPOS.Product;

namespace Grintsys.EasyPOS
{
    public class EasyPOSApplicationAutoMapperProfile : Profile
    {
        public EasyPOSApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateUpdateCustomerDto, CustomerDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<CreateUpdateOrderDto, Order>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<CreateUpdateOrderItemDto, OrderItem>();
        }
    }
}
