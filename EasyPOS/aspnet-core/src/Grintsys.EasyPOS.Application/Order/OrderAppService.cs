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

        public override Task<OrderDto> CreateAsync(CreateUpdateOrderDto input)
        {
            return base.CreateAsync(input);
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
        
        public override async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await _orderRepository.GetOrdersByIdAsync(id);
            var dto = ObjectMapper.Map<Order, OrderDto>(order);
            return dto;
        }

        public override async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Order.CustomerName);
            }

            var orders = await _orderRepository.GetOrdersAsync();

            //orders = orders
            //    .OrderBy(x => x.GetType().GetProperty(input.Sorting)?.GetValue(x, null))
            //    .Skip(input.SkipCount)
            //    .Take(input.MaxResultCount) as List<DebitNote>;

            var ordersDto = await MapToGetListOutputDtosAsync(orders);

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<OrderDto>(
                totalCount,
                ordersDto
            );
        }
    }
}
