﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grintsys.EasyPOS.Product
{
    public class CreateUpdateProductDto
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        [Range(1, float.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float SalePrice { get; set; }
        [Range(0.0, 1.0, ErrorMessage = "Please enter a value between 0.0 and 1.0")]
        public bool Taxes { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public List<CreateUpdateProductWarehouseDto> ProductWarehouses { get; set; } = new List<CreateUpdateProductWarehouseDto>();
    }
}
