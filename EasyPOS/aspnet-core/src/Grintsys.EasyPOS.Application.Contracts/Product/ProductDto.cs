﻿using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Product
{
    public class ProductDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public float SalePrice { get; set; }
        public bool Taxes { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public int Inventory { get; set; }
        public List<ProductWarehouseDto> ProductWarehouse { get; set; } = new List<ProductWarehouseDto>();
    }
}
