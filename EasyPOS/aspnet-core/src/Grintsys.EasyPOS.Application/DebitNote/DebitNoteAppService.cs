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

        public async Task<List<DebitNoteDto>> GetDebitNoteList(string filter)
        {
            var notes = await _debitNoteRepository.GetDebitNotesAsync();
            var dto = new List<DebitNoteDto>(ObjectMapper.Map<List<DebitNote>, List<DebitNoteDto>>(notes));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.CustomerName.ToLower().Contains(filter))
                    .OrderBy(x => x.CustomerName).ToList();
            }

            return dto;
        }

        public async Task<List<DebitNoteDto>> GetDebitNoteListByOrder(string filter, Guid orderId)
        {
            var notes = await _debitNoteRepository.GetDebitNotesByOrderAsync(orderId);
            var dto = new List<DebitNoteDto>(ObjectMapper.Map<List<DebitNote>, List<DebitNoteDto>>(notes));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.CustomerName.ToLower().Contains(filter))
                    .OrderBy(x => x.CustomerName).ToList();
            }

            return dto;
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
                    OrderId = orderId,
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
        
        protected override async Task DeleteByIdAsync(Guid id)
        {
            var data = base.GetEntityByIdAsync(id).Result;

            var createUpdateDto = new CreateUpdateDebitNoteDto()
            {
                OrderId = data.OrderId,
                Id = id,
                CustomerId = data.CustomerId,
                State = DocumentState.Cancelled
            };

            await base.UpdateAsync(id, createUpdateDto);
        }
    }
}
