using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.Product
{
    public class EfCoreProductRepository 
        : EfCoreRepository<EasyPOSDbContext, Product, Guid>,
            IProductRepository
    {
        public EfCoreProductRepository(IDbContextProvider<EasyPOSDbContext> dcContextProvider) : base(dcContextProvider)
        {

        }

        public async Task<List<Product>> GetListAsync()
        {
            var dbSet = (await GetQueryableAsync())
                .Include(x => x.ProductWarehouse)
                    .ThenInclude(x => x.Warehouse);
            return await dbSet.ToListAsync();
        }

        public async Task<List<Product>> GetByIds(List<Guid> ids)
        {
            var dbSet = (await GetDbSetAsync())
                .Include(x => x.ProductWarehouse)
                    .ThenInclude(x => x.Warehouse)
                .Where(x => ids.Contains(x.Id)).ToListAsync();
            return await dbSet;
        }
    }
}
