using Grintsys.EasyPOS.CreateUpdateDtos;
using Grintsys.EasyPOS.Dtos;
using Grintsys.EasyPOS.Models;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.AppServices
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
