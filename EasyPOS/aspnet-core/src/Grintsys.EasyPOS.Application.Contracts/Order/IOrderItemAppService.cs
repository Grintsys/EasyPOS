using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Order
{
    public interface IOrderItemAppService :  
        ICrudAppService<
            OrderItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderItemDto
        >
    {
    }
}
