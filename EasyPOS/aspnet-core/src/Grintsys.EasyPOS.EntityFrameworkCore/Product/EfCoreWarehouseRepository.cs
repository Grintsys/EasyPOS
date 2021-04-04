using Grintsys.EasyPOS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Warehouse>> GetByIds(List<Guid> ids)
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.ProductWarehouses)
                    .ThenInclude(x => x.Product);
            return await data.ToListAsync();
        }
    }
}
