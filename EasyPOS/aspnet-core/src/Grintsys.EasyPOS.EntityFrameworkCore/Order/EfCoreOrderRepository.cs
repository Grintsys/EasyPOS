using Grintsys.EasyPOS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.Order
{
    public class EfCoreOrderRepository 
        : EfCoreRepository<EasyPOSDbContext, Order, Guid>,
            IOrderRepository
    {
        public EfCoreOrderRepository(IDbContextProvider<EasyPOSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.OrderItems);
            return await data.ToListAsync();
        }

        public async Task<Order> GetOrdersByIdAsync(Guid id)
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await data;
        }
    }
}
