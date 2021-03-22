using Grintsys.EasyPOS.Enums;
using System;
using System.Threading.Tasks;
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
                State = DocumentState.Cancelled
            };

            await base.UpdateAsync(id, createUpdateDto);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetOrdersByIdAsync(id);
            var dto = ObjectMapper.Map<Order, OrderDto>(order);
            return dto;
        }

        public async Task<PagedResultDto<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            var orderDto = await MapToGetListOutputDtosAsync(orders);
            return new PagedResultDto<OrderDto>(orders.Count, orderDto);
        }

        public override Task<OrderDto> CreateAsync(CreateUpdateOrderDto input)
        {
            return base.CreateAsync(input);
        }
    }
}
