using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.OrderItem
{
    public class OrderItemDto : FullAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
    }
}
