using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.CreditNote
{
    public class EfCoreCreditNoteRepository
        : EfCoreRepository<EasyPOSDbContext, CreditNote, Guid>,
            ICreditNoteRepository
    {
        public EfCoreCreditNoteRepository(
            IDbContextProvider<EasyPOSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<CreditNote>> GetCreditNotesAsync()
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Items)
                .Include(x => x.Customer);
            return await data.ToListAsync();
        }

        public async Task<CreditNote> GetCreditNoteByIdAsync(Guid id)
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.Items)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await data;
        }
    }
}
