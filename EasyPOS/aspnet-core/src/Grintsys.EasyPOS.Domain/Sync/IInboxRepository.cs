using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Sync
{
    public interface IInboxRepository : IRepository<Inbox, Guid>
    {
        Task<List<Inbox>> GetListAsync();
        Task<Inbox> GetAsync(Guid id);
    }
}
