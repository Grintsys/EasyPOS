using Grintsys.EasyPOS.Order;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Grintsys.EasyPOS.Sync
{
    public class OrderChangedHandler
        : ILocalEventHandler<OrderChangedEvent>,
          ITransientDependency
    {
        private readonly ISapManager _sapManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IOutboxRepository _outboxRepository;

        public OrderChangedHandler(ISapManager sapManager, 
            IOrderRepository orderRepository, 
            IOutboxRepository outboxRepository)
        {
            _sapManager = sapManager;
            _orderRepository = orderRepository;
            _outboxRepository = outboxRepository;
        }

        public async Task HandleEventAsync(OrderChangedEvent eventData)
        {
            var orders = eventData.OrderList;
            var outboxId = eventData.OutboxId;
            var outboxMessage = await _outboxRepository.FindAsync(outboxId);

            if (outboxMessage is null)
                throw new ArgumentException("No inbox message was found");

            //TODO map the right warehousecode
            var warehouseCode = "E1";

            foreach (var orderId in orders)
            {
                var order = await _orderRepository.GetAsync(orderId);
                var input = OrderMapper(order, warehouseCode);
                try
                {
                    var sapResponse = await _sapManager.CreateInvoiceAsync(input);

                    if (outboxMessage != null)
                    {
                        outboxMessage.ErrorMessage = sapResponse.Message;
                        outboxMessage.IsDispatched = sapResponse.IsSuccess;
                        await _outboxRepository.UpdateAsync(outboxMessage);
                    }
                }
                catch(Exception e)
                {
                    if (outboxMessage != null)
                    {
                        outboxMessage.ErrorMessage = e.Message;
                        await _outboxRepository.UpdateAsync(outboxMessage);
                    }
                }
            }
        }

        private OrderItemDto OrderItemMapper(Order.OrderItem order)
                => new()
                {
                    Code = order.Code,
                    Name = order.Name,
                    Description = order.Description,
                    Discount = order.Discount,
                    SalePrice = order.SalePrice
                };

        private CreateOrUpdateInvoiceDto OrderMapper(Order.Order order, string whc)
            => new()
            {
                CreatedDate = DateTime.UtcNow,
                CustomerCode = order.Customer.Code,
                CustomerName = order.CustomerName,
                SalesPersonId = 1,
                WarehouseCode = whc,
                Lines = order.Items.Select(OrderItemMapper).ToList()
            };
    };
}
