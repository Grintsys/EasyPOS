using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
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

        public SincronizadorAppService(IRepository<Sincronizador, Guid> repository) : base(repository)
        {
            _syncRepository = repository;
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
    }
}
