using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.DebitNote
{
    public class EfCoreDebitNoteRepository
        : EfCoreRepository<EasyPOSDbContext, DebitNote, Guid>,
            IDebitNoteRepository
    {
        public EfCoreDebitNoteRepository(
            IDbContextProvider<EasyPOSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<DebitNote>> GetDebitNotesAsync()
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Items)
                .Include(x => x.Customer);
            return await data.ToListAsync();
        }

        public async Task<List<DebitNote>> GetDebitNotesByOrderAsync(Guid orderId)
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Items)
                .Include(x => x.Customer)
                .Where(x => x.OrderId == orderId);
            return await data.ToListAsync();
        }

        public async Task<DebitNote> GetDebitNoteByIdAsync(Guid id)
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Items)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await data;
        }
    }
}
