using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.SAP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class SincronizadorAppService :
        CrudAppService<
            Sincronizador,
            SincronizadorDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateSincronizadorDto>,
        ISincronizadorAppService
    {
        private readonly IRepository<Sincronizador, Guid> _syncRepository;
        private readonly ISapManager _sapManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public SincronizadorAppService(IRepository<Sincronizador, 
            Guid> repository, ISapManager sapManager, 
            IBackgroundJobManager backgroundJobManager) : base(repository)
        {
            _syncRepository = repository;
            _sapManager = sapManager;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task<List<SincronizadorDto>> GetSyncList(string filter)
        {
            var data = await _syncRepository.GetListAsync();
            var dto = new List<SincronizadorDto>(ObjectMapper.Map<List<Sincronizador>, List<SincronizadorDto>>(data));

            if (!filter.IsNullOrWhiteSpace())
            {
                //Enum.TryParse("Active", out SyncEstados estado);
                //Enum.TryParse("Active", out Transacciones transacciones);


                //filter = filter.ToLower();
                //dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(),
                //    x => x.FirstName.ToLower().Contains(filter)
                //    || x.LastName.ToLower().Contains(filter)
                //   )
                //    .OrderBy(x => x.FirstName).ToList();
            }
            return dto;
        }

        public override Task<SincronizadorDto> CreateAsync(CreateUpdateSincronizadorDto input)
        {
            input.TenantId = CurrentTenant.Id;
            return base.CreateAsync(input);
        }

        public async Task<List<SincronizadorDto>> Retry(Guid id)
        {
            var dto = await _syncRepository.GetAsync(id);

            if(dto.TipoTransaccion == Transacciones.CreacionOrden)
            {
                var salesOrderDto = JsonConvert.DeserializeObject<CreateOrUpdateSalesOrder>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
                await _backgroundJobManager.EnqueueAsync(_sapManager.CreateSalesOrderAsync(salesOrderDto));
            }
            else if (dto.TipoTransaccion == Transacciones.CreacionNotaCredito)
            {
                var creditNoteDto = JsonConvert.DeserializeObject<CreateOrUpdateSalesOrder>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
                await _backgroundJobManager.EnqueueAsync(_sapManager.CreateCreditNoteAsync(creditNoteDto));
            }
            else if (dto.TipoTransaccion == Transacciones.CreacionNotaDebito)
            {
                var debitNoteDto = JsonConvert.DeserializeObject<CreateOrUpdateSalesOrder>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
                await _backgroundJobManager.EnqueueAsync(_sapManager.CreateCreditNoteAsync(debitNoteDto));
            }
            else if (dto.TipoTransaccion == Transacciones.CreacionCliente)
            {
                var customerDto = JsonConvert.DeserializeObject<CreateOrUpdateCustomer>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
                await _backgroundJobManager.EnqueueAsync(_sapManager.CreateCustomerAsync(customerDto));
            }

            var syncList = await _syncRepository.GetListAsync();
            var syncListDto = new List<SincronizadorDto>(ObjectMapper.Map<List<Sincronizador>, List<SincronizadorDto>>(syncList));
            return syncListDto;
        }
    }
}
