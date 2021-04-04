using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Product
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<List<Product>> GetListAsync();
        Task<List<Product>> GetByIds(List<Guid> ids);
    }
}
