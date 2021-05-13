using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Customer
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task<List<Customer>> GetListAsync();
    }
}
