using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Sincronizador
{
    class SincronizadorAppService :
        CrudAppService<
            Sincronizador,
            SincronizadorDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateSincronizadorDto>,
        ISincronizadorAppService
    {
        public SincronizadorAppService(IRepository<Sincronizador, Guid> repository) : base(repository)
        {
        }
    }
}
