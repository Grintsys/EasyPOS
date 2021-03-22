using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.OrderItem
{
    public class OrderItemAppService : 
        CrudAppService<
            Order.OrderItem,
            OrderItemDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderItemDto
        >,
        IOrderItemAppService
    {
        public OrderItemAppService(IRepository<Order.OrderItem, Guid> repository) : base(repository)
        {
        }
    }
}
