using Grintsys.EasyPOS.Enums;
using System.ComponentModel.DataAnnotations;

namespace Grintsys.EasyPOS.Customer
{
    public class CreateUpdateCustomerDto
    {
        public string FullName { get; set; }

        [StringLength(13)]
        [RegularExpression(@"^(\d{13}|\s*)?$", ErrorMessage = "Id must be numeric")]
        public string IdNumber { get; set; }

        [StringLength(14)]
        [RegularExpression(@"^(\d{14}|\s*)?$", ErrorMessage = "RTN must be numeric")]
        public string RTN { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"^\([0-9]{3}[\-\)][0-9]{4}-[0-9]{4}$", ErrorMessage = "Please enter valid phone No, please see the following example: (504)2509-2421")]
        public string PhoneNumber { get; set; }

        public CustomerStatus Status { get; set; } = CustomerStatus.Created;

        public string Code { get; set; }
    }
}
