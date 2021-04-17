using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.DebitNote
{
    public interface IDebitNoteAppService :
        ICrudAppService<
            DebitNoteDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateDebitNoteDto
        >
    {
        Task<DebitNoteDto> CreateDebitNoteAsync(Guid orderId);
        Task<List<DebitNoteDto>> GetDebitNoteList(string filter);
        Task<List<DebitNoteDto>> GetDebitNoteListByOrder(string filter, Guid orderId);
    }
}
