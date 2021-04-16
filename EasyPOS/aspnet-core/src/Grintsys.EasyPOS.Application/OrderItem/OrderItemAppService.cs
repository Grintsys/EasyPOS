using Grintsys.EasyPOS.Order;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.OrderItem
{
    public class OrderItemAppService : 
        CrudAppService<
            Order.OrderItem,
            OrderItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderItemDto
        >,
        IOrderItemAppService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderItemAppService(IRepository<Order.OrderItem, Guid> repository, IOrderItemRepository orderItemRepository) : base(repository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<List<OrderItemDto>> GetOrderItemsByOrderId(Guid orderId)
        {
            var data = await _orderItemRepository.GetByOrderIdAsync(orderId);
            var dto = ObjectMapper.Map<List<Order.OrderItem>, List<OrderItemDto>>(data);
            return dto;
        }
        
    }
}
