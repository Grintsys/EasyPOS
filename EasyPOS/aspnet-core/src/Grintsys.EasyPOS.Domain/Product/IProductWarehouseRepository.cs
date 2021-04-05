using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Product
{
    public interface IProductWarehouseRepository : IRepository<ProductWarehouse, Guid>
    {
        Task<List<ProductWarehouse>> GetListAsync();

        Task<ProductWarehouse> GetAsync(Guid id);
    }
}
