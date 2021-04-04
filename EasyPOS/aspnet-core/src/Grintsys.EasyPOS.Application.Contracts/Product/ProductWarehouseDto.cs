using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Product
{
    public class ProductWarehouseDto : FullAuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public Guid WarehouseId { get; set; }
        public WarehouseDto Warehouse { get; set; }
        public int Inventory { get; set; }
    }
}
