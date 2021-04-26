using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Product
{
    public class ProductWarehouse : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int Inventory { get; set; }
        public string ProductName => Product?.Name;
        public string WarehouseName => Warehouse?.Name;
    }
}
