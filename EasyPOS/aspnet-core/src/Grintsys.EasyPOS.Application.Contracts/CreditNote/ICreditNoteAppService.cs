using System;
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
    }
}
