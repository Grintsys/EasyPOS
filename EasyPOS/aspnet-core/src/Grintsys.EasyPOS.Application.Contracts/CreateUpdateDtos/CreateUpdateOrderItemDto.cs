using System;
using System.ComponentModel.DataAnnotations;

namespace Grintsys.EasyPOS.CreateUpdateDtos
{
    public class CreateUpdateOrderItemDto
    {
        [Required] 
        public Guid OrderId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public float SalePrice { get; set; }
        [Required]
        public float Taxes { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float TotalItem { get; set; }
    }
}
