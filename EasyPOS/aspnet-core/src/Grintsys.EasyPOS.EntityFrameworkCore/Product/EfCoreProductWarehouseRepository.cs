using Grintsys.EasyPOS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.Product
{
    public class EfCoreProductWarehouseRepository 
        : EfCoreRepository<EasyPOSDbContext, ProductWarehouse, Guid>, IProductWarehouseRepository
    {
        public EfCoreProductWarehouseRepository(IDbContextProvider<EasyPOSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<ProductWarehouse>> GetListAsync()
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Warehouse)
                .Include(x => x.Product);
            return await data.ToListAsync();
        }

        public async Task<List<ProductWarehouse>> GetByIds(List<Guid> ids)
        {
            var dbSet = (await GetDbSetAsync())
                .Where(x => ids.Contains(x.Id))
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .ToListAsync();
            return await dbSet;
        }
    }
}
