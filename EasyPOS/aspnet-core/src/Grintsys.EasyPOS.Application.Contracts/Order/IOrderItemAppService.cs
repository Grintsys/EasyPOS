using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<List<OrderItemDto>> GetOrderItemsByOrderId(Guid orderId);
    }
}
