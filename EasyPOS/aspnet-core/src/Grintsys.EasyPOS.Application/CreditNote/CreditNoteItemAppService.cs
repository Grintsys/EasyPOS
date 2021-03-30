using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNoteItemAppService :
        CrudAppService<
            CreditNoteItem,
            CreditNoteItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCreditNoteItemDto>,
        ICreditNoteItemAppService
    {
        public CreditNoteItemAppService(IRepository<CreditNoteItem, Guid> repository) : base(repository)
        {
        }
    }
}
