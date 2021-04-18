using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Product
{
    public class WarehouseDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<ProductWarehouseDto> ProductWarehouses { get; set; } = new List<ProductWarehouseDto>();
    }
}
