using System;
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
    }
}
