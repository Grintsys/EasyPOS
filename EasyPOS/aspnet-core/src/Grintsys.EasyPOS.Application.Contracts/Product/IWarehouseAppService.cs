using System;
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
    }
}
