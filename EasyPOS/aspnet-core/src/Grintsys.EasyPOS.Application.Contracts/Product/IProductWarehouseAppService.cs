using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Product
{
    public interface IProductWarehouseAppService :
        ICrudAppService<
            ProductWarehouseDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductWarehouseDto>
    {
    }

}
