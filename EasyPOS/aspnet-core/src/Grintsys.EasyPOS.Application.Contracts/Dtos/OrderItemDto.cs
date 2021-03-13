using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Dtos
{
    public class OrderItemDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public float SalePrice { get; set; }
        public float Taxes { get; set; }
        public int Quantity { get; set; }
        public float TotalItem { get; set; }
    }
}
