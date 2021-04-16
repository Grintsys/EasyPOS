using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Order
{
    public interface IOrderItemRepository : IRepository<OrderItem, Guid>
    {
        Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId);
    }
}