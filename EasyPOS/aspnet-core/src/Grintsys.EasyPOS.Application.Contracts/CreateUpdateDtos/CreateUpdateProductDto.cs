using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Grintsys.EasyPOS.CreateUpdateDtos
{
    public class CreateUpdateProductDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float SalePrice { get; set; }

        [Required]
        [Range(0.0, 1.0, ErrorMessage = "Please enter a value between 0.0 and 1.0")]
        public float Taxes { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
