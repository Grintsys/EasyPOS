using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepository<PaymentMethod.PaymentMethod, Guid> _paymentMethodRepository;

        public OrderAppService(IRepository<Order, Guid> repository, 
            IOrderRepository orderRepository, 
            IRepository<PaymentMethod.PaymentMethod, Guid> personRepository)
            : base(repository)
        {
            _orderRepository = orderRepository;
            _paymentMethodRepository = personRepository;
        }

        public override async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await _orderRepository.GetOrdersByIdAsync(id);
            var dto = ObjectMapper.Map<Order, OrderDto>(order);
            return dto;
        }

        public override Task<OrderDto> CreateAsync(CreateUpdateOrderDto input)
        {
                var orderId = input.Id;

                var order = base.CreateAsync(input);

                var paymentMethodDto = ObjectMapper.Map<CreateUpdatePaymentMethodDto, PaymentMethod.PaymentMethod>(input.PaymentMethods);
                paymentMethodDto.OrderId = orderId;

                _paymentMethodRepository.InsertAsync(paymentMethodDto);

                return order;
        }

        public async Task<List<OrderDto>> GetOrderList(string filter)
        {
            var orders = await _orderRepository.GetOrdersAsync();
            var dto = new List<OrderDto>(ObjectMapper.Map<List<Order>, List<OrderDto>>(orders));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), 
                        x => x.CustomerName.ToLower().Contains(filter))
                    .OrderBy(x => x.CustomerName).ToList();
            }

            return dto;
        }
        
        protected override async Task DeleteByIdAsync(Guid id)
        {
            var order = base.GetEntityByIdAsync(id).Result;

            var createUpdateDto = new CreateUpdateOrderDto()
            {
                Id = id,
                CustomerId = order.CustomerId,
                State = DocumentState.Cancelled,
                OrderType = order.OrderType
                
            };

            await base.UpdateAsync(id, createUpdateDto);
        }
        
        public async Task<OrderDocumentDto> GetOrderDocuments(Guid orderId)
        {
            var order = await _orderRepository.GetOrdersByIdAsync(orderId);
            var dto = ObjectMapper.Map<Order, OrderDto>(order);

            var documents = new OrderDocumentDto()
            {
                Id = orderId,
                CreditNotes = dto.CreditNotes
            };
            
            return documents;
        }
    }
}
