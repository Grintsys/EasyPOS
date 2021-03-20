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
            CreateMap<Product.Product, ProductDto>();
            CreateMap<Product.Product, ProductLookupDto>();
            CreateMap<CreateUpdateProductDto, Product.Product>();
            CreateMap<Customer.Customer, CustomerDto>();
            CreateMap<Customer.Customer, CustomerLookupDto>();
            CreateMap<CreateUpdateCustomerDto, Customer.Customer>();
            CreateMap<Order.Order, OrderDto>();
            CreateMap<CreateUpdateOrderDto, Order.Order>();
            CreateMap<OrderItem.OrderItem, OrderItemDto>();
            CreateMap<CreateUpdateOrderItemDto, OrderItem.OrderItem>();
        }
    }
}
