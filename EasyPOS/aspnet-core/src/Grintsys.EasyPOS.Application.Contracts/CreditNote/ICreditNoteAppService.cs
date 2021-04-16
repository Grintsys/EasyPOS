using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.CreditNote
{
    public interface ICreditNoteAppService :
        ICrudAppService<
            CreditNoteDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCreditNoteDto
        >
    {
        Task<CreditNoteDto> CreateCreditNoteAsync(Guid orderId);
        Task<List<CreditNoteDto>> GetCreditNoteList(string filter);
        Task<List<CreditNoteDto>> GetCreditNoteListByOrder(string filter, Guid orderId);
    }
}
