using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.SAP;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.TipoTransaccion.ToString().ToLower().Contains(filter)
                    || x.Estado.ToString().ToLower().Contains(filter)
                    || (!string.IsNullOrEmpty(x.Data) && x.Data.ToLower().Contains(filter))
                    || (!string.IsNullOrEmpty(x.Message) && x.Message.ToLower().Contains(filter))
                    ).ToList();
            }
            return dto.OrderByDescending(x => x.CreationTime).ToList();
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
                var salesOrderDto = JsonConvert.DeserializeObject<SalesOrderSapArgs>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
                await _backgroundJobManager.EnqueueAsync(salesOrderDto);
            }
            else if (dto.TipoTransaccion == Transacciones.CreacionNotaCredito)
            {
                var creditNoteDto = JsonConvert.DeserializeObject<CreaditNodeSapArgs>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
                await _backgroundJobManager.EnqueueAsync(creditNoteDto);
            }
            //else if (dto.TipoTransaccion == Transacciones.CreacionNotaDebito)
            //{
            //    var debitNoteDto = JsonConvert.DeserializeObject<CreateOrUpdateSalesOrder>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
            //    await _backgroundJobManager.EnqueueAsync(debitNoteDto);
            //}
            else if (dto.TipoTransaccion == Transacciones.CreacionCliente)
            {
                var customerDto = JsonConvert.DeserializeObject<CustomerSapArgs>(string.IsNullOrEmpty(dto.Data) ? "{}" : dto.Data);
                await _backgroundJobManager.EnqueueAsync(customerDto);
            }

            var syncList = await _syncRepository.GetListAsync();
            var syncListDto = new List<SincronizadorDto>(ObjectMapper.Map<List<Sincronizador>, List<SincronizadorDto>>(syncList));
            
            return syncListDto;
        }

        [Authorize("Ver/Modificar_Sincronizaciones")]
        public override Task<SincronizadorDto> UpdateAsync(Guid id, CreateUpdateSincronizadorDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
