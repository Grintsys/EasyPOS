using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grintsys.EasyPOS.Enums;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Order
{
    public class OrderAppService : 
        CrudAppService<
            Order,
            OrderDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderDto
        >, 
        IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderAppService(IRepository<Order, Guid> repository, IOrderRepository orderRepository) 
            : base(repository)
        {
            _orderRepository = orderRepository;
        }


        protected override async Task DeleteByIdAsync(Guid id)
        {
            var order = base.GetEntityByIdAsync(id).Result;

            var createUpdateDto = new CreateUpdateOrderDto()
            {
                CustomerId = order.CustomerId,
                OrderState = OrderStates.Cancelled
            };

            await base.UpdateAsync(id, createUpdateDto);
        }

        protected override async Task<Order> GetEntityByIdAsync(Guid id)
        {
            return await _orderRepository.GetOrdersByIdAsync(id);
        }

        public async Task<PagedResultDto<OrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            var orderDto = await MapToGetListOutputDtosAsync(orders);
            return new PagedResultDto<OrderDto>(orders.Count, orderDto);
        }
    }
}
