﻿using Grintsys.EasyPOS.CreateUpdateDtos;
using Grintsys.EasyPOS.Dtos;
using Grintsys.EasyPOS.Models;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.AppServices
{
    public class OrderAppService : 
        CrudAppService<
            Order,
            OrderDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderDto
        >, 
        IOrderAppService
    {
        public OrderAppService(IRepository<Order, Guid> repository) : base(repository)
        {
        }
    }
}
