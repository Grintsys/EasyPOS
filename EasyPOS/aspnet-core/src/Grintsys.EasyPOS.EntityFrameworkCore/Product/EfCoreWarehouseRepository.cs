using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.Product
{
    public class EfCoreWarehouseRepository
        : EfCoreRepository<EasyPOSDbContext, Warehouse, Guid>, IWarehouseRepository
    {
        public EfCoreWarehouseRepository(IDbContextProvider<EasyPOSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Warehouse>> GetListAsync()
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.ProductWarehouses);
            return await data.ToListAsync();
        }

        public async Task<List<Warehouse>> GetWarehousesAsync()
        {
            var data = (await GetQueryableAsync());
            return await data.ToListAsync();
        }

        public async Task<Warehouse> GetAsync(Guid id)
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.ProductWarehouses)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await data;
        }
    }
}
