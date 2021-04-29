using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ProductWarehouse> GetAsync(Guid id)
        {
            var dbSet = (await GetQueryableAsync())
                .Where(x => id  == x.Id)
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await dbSet;
        }

        public async Task<ProductWarehouse> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId)
        {
            var dbSet = (await GetQueryableAsync())
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.WarehouseId == warehouseId);
            return await dbSet;
        }
    }
}
