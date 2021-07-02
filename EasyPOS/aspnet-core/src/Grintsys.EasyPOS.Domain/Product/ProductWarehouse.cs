using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.Product
{
    public class ProductWarehouse : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int Inventory { get; set; }
        public string ProductName => Product?.Name;
        public string WarehouseName => Warehouse?.Name;
    }
}
