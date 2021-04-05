using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Product
{
    public class ProductWarehouseDto : FullAuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int Inventory { get; set; }
    }
}
