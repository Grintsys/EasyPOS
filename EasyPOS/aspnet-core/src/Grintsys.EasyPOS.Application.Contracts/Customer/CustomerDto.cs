﻿using System;
using Grintsys.EasyPOS.Enums;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Customer
{
    public class CustomerDto : FullAuditedEntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string RTN { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public CustomerStatus Status { get; set; }
        public string Code { get; set; }
        public string FullName => FirstName + " " + LastName;
    }
}
