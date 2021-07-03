using Grintsys.EasyPOS.Document;
using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.Order;
using Grintsys.EasyPOS.SAP;
using Grintsys.EasyPOS.Sincronizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
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
        private readonly IBackgroundJobManager _backgroundJobManager;

        public CreditNoteAppService(
            IRepository<CreditNote, Guid> repository,
            ICreditNoteRepository creditNoteRepository,
            IOrderRepository orderRepository,
            IBackgroundJobManager backgroundJobManager) : base(repository)
        {
            _creditNoteRepository = creditNoteRepository;
            _orderRepository = orderRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        public override async Task<CreditNoteDto> GetAsync(Guid id)
        {
            var order = await _creditNoteRepository.GetCreditNoteByIdAsync(id);
            var dto = ObjectMapper.Map<CreditNote, CreditNoteDto>(order);
            return dto;
        }

        public override async Task<CreditNoteDto> CreateAsync(CreateUpdateCreditNoteDto input)
        {
            input.State = DocumentState.Transferred;
            input.TenantId = CurrentTenant.Id;

            var document = await base.CreateAsync(input);

            await _backgroundJobManager.EnqueueAsync(new CreaditNodeSapArgs()
            {
                CreatedDate = document.CreationTime,
                CustomerCode = input.CustomerCode,
                CustomerName = input.CustomerName,
                SalesPersonId = 1,
                WarehouseCode = input.WarehouseCode,
                Lines = input.Items.Select(x => Map(x)).ToList()
            });

            return document;
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
                var createUpdateDto = new CreateUpdateCreditNoteDto()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = order.CustomerId,
                    State = DocumentState.Created,
                    OrderId = orderId,
                    Items = order.Items.Select(x => ObjectMapper.Map<
                        Order.OrderItem, CreateUpdateCreditNoteItemDto>(x)).ToList()
                };

                return await base.CreateAsync(createUpdateDto);
            }

            return null;
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

        private OrderItemDto Map(CreateUpdateCreditNoteItemDto item)
        {
            return new OrderItemDto()
            {
                ProductId = item.ProductId,
                Name = item.Name,
                Description = item.Description,
                Code = item.Code,
                Quantity = item.Quantity,
                SalePrice = item.SalePrice,
                Taxes = item.Taxes,
                Discount = item.Discount,
                TotalItem = item.TotalItem
            };
        }
    }
}
