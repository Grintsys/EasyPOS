using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Product
{
    public interface IWarehouseAppService :
        ICrudAppService<
            WarehouseDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateWarehouseDto>
    {
        Task<List<WarehouseDto>> GetWarehouseList(string filter);
    }
}
