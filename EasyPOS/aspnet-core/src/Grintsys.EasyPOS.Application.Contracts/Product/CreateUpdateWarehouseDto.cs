using System;

namespace Grintsys.EasyPOS.Product
{
    public class CreateUpdateWarehouseDto
    {
        public Guid? TenantId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
