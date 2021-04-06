using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Product
{
    public class Warehouse : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<ProductWarehouse> ProductWarehouses { get; set; }
    }
}
