using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Grintsys.EasyPOS.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Customer
{
    public class Customer : FullAuditedAggregateRoot<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string RTN { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public CustomerStatus Status { get; set; }
        public string Code { get; set; }
        public ICollection<Order.Order> Orders { get; set; } = new List<Order.Order>();
    }
}
