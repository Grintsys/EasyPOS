using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Sincronizador
{
    public interface ISincronizadorAppService :
        ICrudAppService<
            SincronizadorDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateSincronizadorDto
        >
    {
        Task<List<SincronizadorDto>> GetSyncList(string filter);
        Task<List<SincronizadorDto>> Retry(Guid id);
    }
}
