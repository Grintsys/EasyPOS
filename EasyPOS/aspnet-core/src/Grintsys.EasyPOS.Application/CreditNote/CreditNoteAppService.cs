using Grintsys.EasyPOS.Document;
using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.Order;
using System;
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

        public override async Task<PagedResultDto<CreditNoteDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(CreditNote.CustomerName);
            }

            var creditNotes = await _creditNoteRepository.GetCreditNotesAsync();

            //creditNotes = creditNotes
                //.OrderBy(x => x.GetType().GetProperty(input.Sorting)?.GetValue(x, null)) 
                //.Skip(input.SkipCount)
                //.Take(input.MaxResultCount) as List<CreditNote>; //TODO

            var creditNoteDto = await MapToGetListOutputDtosAsync(creditNotes);

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<CreditNoteDto>(
                totalCount,
                creditNoteDto
            );
        }

        public override Task<CreditNoteDto> CreateAsync(CreateUpdateCreditNoteDto input)
        {
            return base.CreateAsync(input);
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
                    Items = order.Items.Select(x => Map(x, creditNoteId)).ToList()
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
    }
}
