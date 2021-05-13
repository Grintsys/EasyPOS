using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Sync
{
    public class InboxAppService :
        CrudAppService<
            Inbox,
            InboxDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateOrUpdateInboxDto
        >
    {
        private readonly IInboxRepository _Repository;
        public InboxAppService(IRepository<Inbox, Guid> repository,
            IInboxRepository productRepository) : base(repository)
        {
            _Repository = productRepository;
        }

        public async Task<InboxDto> GetInbox(Guid id)
        {
            var item = await _Repository.GetAsync(id);
            var dto = ObjectMapper.Map<Inbox, InboxDto>(item);
            return dto;
        }

        public async Task<List<InboxDto>> GetInboxList(string filter)
        {
            var items = await _Repository.GetListAsync();
            var dto = new List<InboxDto>(ObjectMapper.Map<List<Inbox>, List<InboxDto>>(items));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), x => x.MessageType.ToLower().Contains(filter))
                         .OrderBy(x => x.CreationTime).ToList();
            }

            return dto;
        }
    }
}
