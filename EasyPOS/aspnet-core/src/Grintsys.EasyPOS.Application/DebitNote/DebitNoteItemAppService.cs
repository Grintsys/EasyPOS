using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNoteItemAppService :
        CrudAppService<
            DebitNoteItem,
            DebitNoteItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateDebitNoteItemDto>,
        IDebitNoteItemAppService
    {
        public DebitNoteItemAppService(IRepository<DebitNoteItem, Guid> repository) : base(repository)
        {
        }
    }
}
