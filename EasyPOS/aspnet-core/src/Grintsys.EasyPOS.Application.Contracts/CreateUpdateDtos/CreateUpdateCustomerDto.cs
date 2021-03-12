using Grintsys.EasyPOS.Enums;
using System.ComponentModel.DataAnnotations;

namespace Grintsys.EasyPOS.CreateUpdateDtos
{
    public class CreateUpdateCustomerDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(13)]
        [RegularExpression("[^0-9]", ErrorMessage = "Id must be numeric")]
        public string IdNumber { get; set; }

        [Required]
        [StringLength(14)]
        [RegularExpression("[^0-9]", ErrorMessage = "RTN must be numeric")]
        public string RTN { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^(504)(?!0+$)(+\d{1,3}[- ]?)?(?!0+$)\d{8,8}$", ErrorMessage = "Please enter valid phone No, please see the following example: (504)2509-2421")]
        public string PhoneNumber { get; set; }

        [Required]
        public CustomerStatus Status { get; set; } = CustomerStatus.Created;

        [Required]
        public string Code { get; set; }
    }
}
