using Grintsys.EasyPOS.Document;
using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNoteAppService :
        CrudAppService<
            CreditNote,
            CreditNoteDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCreditNoteDto>,
        ICreditNoteAppService
    {
        private readonly ICreditNoteRepository _creditNoteRepository;
        private readonly IOrderRepository _orderRepository;

        public CreditNoteAppService(
            IRepository<CreditNote, Guid> repository, 
            ICreditNoteRepository creditNoteRepository, 
            IOrderRepository orderRepository) : base(repository)
        {
            _creditNoteRepository = creditNoteRepository;
            _orderRepository = orderRepository;
        }

        public override async Task<CreditNoteDto> GetAsync(Guid id)
        {
            var order = await _creditNoteRepository.GetCreditNoteByIdAsync(id);
            var dto = ObjectMapper.Map<CreditNote, CreditNoteDto>(order);
            return dto;
        }

        public async Task<List<CreditNoteDto>> GetCreditNoteList(string filter)
        {
            var notes = await _creditNoteRepository.GetCreditNotesAsync();
            var dto = new List<CreditNoteDto>(ObjectMapper.Map<List<CreditNote>, List<CreditNoteDto>>(notes));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.CustomerName.ToLower().Contains(filter))
                    .OrderBy(x => x.CustomerName).ToList();
            }

            return dto;
        }

        public async Task<List<CreditNoteDto>> GetCreditNoteListByOrder(string filter, Guid orderId)
        {
            var notes = await _creditNoteRepository.GetCreditNotesByOrderAsync(orderId);
            var dto = new List<CreditNoteDto>(ObjectMapper.Map<List<CreditNote>, List<CreditNoteDto>>(notes));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.CustomerName.ToLower().Contains(filter))
                    .OrderBy(x => x.CustomerName).ToList();
            }

            return dto;
        }

        public async Task<CreditNoteDto> CreateCreditNoteAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrdersByIdAsync(orderId);

            if (order != null)
            {
                var creditNoteId = Guid.NewGuid();
                var createUpdateDto = new CreateUpdateCreditNoteDto()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = order.CustomerId,
                    State = DocumentState.Created,
                    Items = order.Items.Select(x => Map(x, creditNoteId)).ToList(),
                    OrderId = orderId
                };

                return await base.CreateAsync(createUpdateDto);
            }

            return null;
        }


        private static CreateUpdateCreditNoteItemDto Map(DocumentItem item, Guid creditNoteId)
        {
            return new()
            {
                Name = item.Name,
                Description = item.Description,
                Code = item.Code,
                SalePrice = item.SalePrice,
                Taxes = item.Taxes,
                Quantity = item.Quantity,
                Discount = item.Discount,
                TotalItem = item.TotalItem,
                CreditNoteId = creditNoteId
            };
        }
        
        protected override async Task DeleteByIdAsync(Guid id)
        {
            var data = base.GetEntityByIdAsync(id).Result;

            var createUpdateDto = new CreateUpdateCreditNoteDto()
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
