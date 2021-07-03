using System;
using System.Threading.Tasks;
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

        public override Task<DebitNoteItemDto> CreateAsync(CreateUpdateDebitNoteItemDto input)
        {
            input.TenantId = CurrentTenant.Id;
            return base.CreateAsync(input);
        }
    }
}
