using System;

namespace Grintsys.EasyPOS.Product
{
    public class CreateUpdateProductWarehouseDto
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Inventory { get; set; }
    }
}
