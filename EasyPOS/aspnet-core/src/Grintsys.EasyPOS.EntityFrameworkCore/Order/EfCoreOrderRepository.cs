using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.Order
{
    public class EfCoreOrderRepository 
        : EfCoreRepository<EasyPOSDbContext, Order, Guid>,
            IOrderRepository
    {
        public EfCoreOrderRepository(IDbContextProvider<EasyPOSDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Items)
                .Include(x => x.Customer)
                .Include(x => x.DebitNotes)
                    .ThenInclude(x => x.Items)
                .Include(x => x.CreditNotes)
                    .ThenInclude(x => x.Items)
                .Include(x => x.PaymentMethods);
            return await data.ToListAsync();
        }

        public async Task<Order> GetOrdersByIdAsync(Guid id)
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Items)
                .Include(x => x.Customer)
                .Include(x => x.DebitNotes)
                    .ThenInclude(x => x.Items)
                .Include(x => x.CreditNotes)
                    .ThenInclude(x => x.Items)
                .Include(x => x.PaymentMethods)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await data;
        }
    }
}
