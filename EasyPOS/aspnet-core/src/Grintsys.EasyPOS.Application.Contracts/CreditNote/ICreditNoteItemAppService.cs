using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.CreditNote
{
    public interface ICreditNoteItemAppService :
        ICrudAppService<
            CreditNoteItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCreditNoteItemDto
        >
    {
    }
}
