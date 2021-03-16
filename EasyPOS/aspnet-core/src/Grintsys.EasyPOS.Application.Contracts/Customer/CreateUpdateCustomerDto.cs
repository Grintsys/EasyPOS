using System.ComponentModel.DataAnnotations;
using Grintsys.EasyPOS.Enums;

namespace Grintsys.EasyPOS.Customer
{
    public class CreateUpdateCustomerDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(13)]
        [RegularExpression(@"^(\d{13}|\s*)?$", ErrorMessage = "Id must be numeric")]
        public string IdNumber { get; set; }

        [Required]
        [StringLength(14)]
        [RegularExpression(@"^(\d{14}|\s*)?$", ErrorMessage = "RTN must be numeric")]
        public string RTN { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\([0-9]{3}[\-\)][0-9]{4}-[0-9]{4}$", ErrorMessage = "Please enter valid phone No, please see the following example: (504)2509-2421")]
        public string PhoneNumber { get; set; }

        public CustomerStatus Status { get; set; } = CustomerStatus.Created;

        [Required]
        public string Code { get; set; }
    }
}
