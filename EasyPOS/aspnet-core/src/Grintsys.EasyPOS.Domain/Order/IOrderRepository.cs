using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Order
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrdersByIdAsync(Guid id);
    }
}
