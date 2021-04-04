using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Product
{
    public interface IWarehouseRepository : IRepository<Warehouse, Guid>
    {
        Task<List<Warehouse>> GetListAsync();
        Task<Warehouse> GetAsync(Guid id);
    }
}
