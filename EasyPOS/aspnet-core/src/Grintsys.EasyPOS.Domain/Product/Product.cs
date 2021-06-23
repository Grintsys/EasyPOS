using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Product
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public float SalePrice { get; set; }
        public bool Taxes { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductWarehouse> ProductWarehouse { get; set; } = new List<ProductWarehouse>();
    }
}