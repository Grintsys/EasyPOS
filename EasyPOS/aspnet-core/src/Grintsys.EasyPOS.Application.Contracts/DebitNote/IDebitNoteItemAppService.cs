using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.DebitNote
{
    public interface IDebitNoteItemAppService :
        ICrudAppService<
            DebitNoteItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateDebitNoteItemDto
        >
    {
    }
}
