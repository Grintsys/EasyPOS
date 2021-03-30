using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grintsys.EasyPOS.CreditNote
{
    public interface ICreditNoteRepository
    {
        Task<List<CreditNote>> GetCreditNotesAsync();
        Task<CreditNote> GetCreditNoteByIdAsync(Guid id);
    }
}
