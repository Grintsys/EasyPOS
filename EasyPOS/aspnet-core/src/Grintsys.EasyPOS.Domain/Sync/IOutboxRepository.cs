using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Sync
{
    public interface IOutboxRepository : IRepository<Outbox, Guid>
    {
        Task<List<Outbox>> GetListAsync();
        Task<Outbox> GetAsync(Guid id);
    }
}
