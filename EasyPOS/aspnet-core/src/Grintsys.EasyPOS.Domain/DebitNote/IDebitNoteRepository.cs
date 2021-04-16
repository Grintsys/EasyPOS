using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grintsys.EasyPOS.DebitNote
{
    public interface IDebitNoteRepository
    {
        Task<List<DebitNote>> GetDebitNotesAsync();
        Task<List<DebitNote>> GetDebitNotesByOrderAsync(Guid orderId);
        Task<DebitNote> GetDebitNoteByIdAsync(Guid id);
    }
}
