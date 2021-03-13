using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Dtos
{
    public class OrderDto : FullAuditedEntityDto<Guid>
    {
        public Guid CustomerId { get; set; }
        public OrderStates OrderState { get; set; }
        public float SubTotal { get; set; }
        public float ISV { get; set; }
        public float Total { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
