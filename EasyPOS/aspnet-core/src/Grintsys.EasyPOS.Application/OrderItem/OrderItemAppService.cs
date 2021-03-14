using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.OrderItem
{
    public class OrderItemAppService : 
        CrudAppService<
            OrderItem,
            OrderItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderItemDto
        >,
        IOrderItemAppService
    {
        public OrderItemAppService(IRepository<OrderItem, Guid> repository) : base(repository)
        {
        }
    }
}
