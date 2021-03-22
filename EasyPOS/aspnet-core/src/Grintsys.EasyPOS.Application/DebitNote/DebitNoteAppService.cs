using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNoteAppService :
        CrudAppService<
            DebitNote,
            DebitNoteDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateDebitNoteDto>,
        IDebitNoteAppService
    {
        private readonly IDebitNoteRepository _debitNoteRepository;
        private readonly IOrderRepository _orderRepository;

        public DebitNoteAppService(
            IRepository<DebitNote, Guid> repository, 
            IDebitNoteRepository debitNoteRepository, 
            IOrderRepository orderRepository) : base(repository)
        {
            _debitNoteRepository = debitNoteRepository;
            _orderRepository = orderRepository;
        }

        public override async Task<DebitNoteDto> GetAsync(Guid id)
        {
            var order = await _debitNoteRepository.GetDebitNoteByIdAsync(id);
            var dto = ObjectMapper.Map<DebitNote, DebitNoteDto>(order);
            return dto;
        }

        public override async Task<PagedResultDto<DebitNoteDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(DebitNote.CustomerName);
            }

            var debitNotes = await _debitNoteRepository.GetDebitNotesAsync();
            
            //debitNotes = debitNotes
            //    .OrderBy(x => x.GetType().GetProperty(input.Sorting)?.GetValue(x, null))
            //    .Skip(input.SkipCount)
            //    .Take(input.MaxResultCount) as List<DebitNote>;

            var debitNoteDto = await MapToGetListOutputDtosAsync(debitNotes);

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<DebitNoteDto>(
                totalCount,
                debitNoteDto
            );
        }

        public async Task<DebitNoteDto> CreateDebitNoteAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrdersByIdAsync(orderId);

            if (order != null)
            {
                var createUpdateDto = new CreateUpdateDebitNoteDto()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = order.CustomerId,
                    State = DocumentState.Created,
                    Items = order.Items.Select(x => ObjectMapper.Map<
                        Order.OrderItem, CreateUpdateDebitNoteItemDto>(x)).ToList()
                };

                foreach (var item in createUpdateDto.Items)
                {
                    item.DebitNoteId = createUpdateDto.Id;
                }

                return await base.CreateAsync(createUpdateDto);
            }

            return null;
        }
    }
}
