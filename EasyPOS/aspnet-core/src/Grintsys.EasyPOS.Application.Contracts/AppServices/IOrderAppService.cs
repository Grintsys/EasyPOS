using Grintsys.EasyPOS.CreateUpdateDtos;
using Grintsys.EasyPOS.Dtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.AppServices
{
    public interface IOrderAppService :
        ICrudAppService<
            OrderDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderDto
        >
    {
    }
}
