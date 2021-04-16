using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.Order
{
    public class EfCoreOrderItemRepository : EfCoreRepository<EasyPOSDbContext, OrderItem, Guid>,
        IOrderItemRepository
    {
        public EfCoreOrderItemRepository(IDbContextProvider<EasyPOSDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
                
        public async Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId){
            var data = (await GetQueryableAsync())
                .Where(x => x.OrderId == orderId).ToListAsync();
            return await data;
        }
    }
}