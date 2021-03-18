using System.ComponentModel.DataAnnotations;

namespace Grintsys.EasyPOS.Product
{
    public class CreateUpdateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float SalePrice { get; set; }

        [Range(0.0, 1.0, ErrorMessage = "Please enter a value between 0.0 and 1.0")]
        public float Taxes { get; set; }

        public bool IsActive { get; set; }

        public string ImageUrl { get; set; }
    }
}
