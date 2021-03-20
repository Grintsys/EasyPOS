﻿using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Order
{
    public interface IOrderAppService :
        ICrudAppService<
            OrderDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderDto
        >
    {
        Task<PagedResultDto<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(Guid id);
    }
}
