using Grintsys.EasyPOS.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Local;

namespace Grintsys.EasyPOS.Sync
{
    public class SyncAppService :
      ApplicationService,
      ISyncAppService,
      ITransientDependency
    {

        private readonly IInboxRepository _Repository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILocalEventBus _localEventBus;
        private readonly ISapManager _sapManager;

        //private readonly IOutboxRepository _Repository;
        public SyncAppService(ILocalEventBus localEventBus, IOrderRepository orderRepository, ISapManager sapManager)
        {
            _localEventBus = localEventBus;
            _orderRepository = orderRepository;
            _sapManager = sapManager;
        }

        public Task SyncAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual async Task SyncInboxAsync()
        {
            //PUBLISH THE EVENT
            await _localEventBus.PublishAsync(
                new PullProductChangedEvent()
            );

            await _localEventBus.PublishAsync(
               new PullCustomerChangedEvent()
            );
        }

        public virtual async Task SyncOutboxAsync()
        {
            int a = 1;
            //PUBLISH THE EVENT
            await _localEventBus.PublishAsync(
                new OrderChangedEvent()
                {
                    OutboxId = new Guid("9cc79c9b-d7dc-4d02-894d-0c1b400fc502"),
                    OrderList = new List<Guid>()
                    {
                        new Guid("9cc79c9b-d7dc-4d02-894d-0c1b400fc502")
                    }
                }
            );

            /*
            await _localEventBus.PublishAsync(
                new CustomerChangedEvent()
            );
            */
        }

        public virtual async Task SyncOutboxAsync(Guid id)
        {
                var warehouseCode = "E1";
                var order = await _orderRepository.GetAsync(id);
                var input = OrderMapper(order, warehouseCode);

                try
                {
                    var sapResponse = await _sapManager.CreateInvoiceAsync(input);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Error occurred, please contact admin");
                }
        }

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

        private OrderItemDto OrderItemMapper(Order.OrderItem order)
               => new()
               {
                   Code = order.Code,
                   Name = order.Name,
                   Description = order.Description,
                   Discount = order.Discount,
                   SalePrice = order.SalePrice
               };

    }
}
