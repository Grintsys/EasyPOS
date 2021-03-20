using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.OrderItem
{
    public class OrderItemDto : FullAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public float SalePrice { get; set; }
        public float Taxes { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public float TotalItem { get; set; }
    }
}
