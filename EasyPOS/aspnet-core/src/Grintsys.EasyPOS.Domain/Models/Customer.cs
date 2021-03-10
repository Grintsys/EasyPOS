﻿using Grintsys.EasyPOS.Enums;

namespace Grintsys.EasyPOS.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string RTN { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public CustomerStatus Status { get; set; }
        public string Code { get; set; }
    }
}
