using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Document
{
    public class Document<T> : FullAuditedAggregateRoot<Guid>
    {
        public DocumentState State { get; set; } = DocumentState.Created;
        public ICollection<T> Items { get; set; } = new List<T>();
        public virtual float ISV { get; }
        public virtual float Discount { get; }
        public virtual float SubTotal { get; }
        public float Total => SubTotal + ISV - Discount;
        public Guid CustomerId { get; set; }
        public Customer.Customer Customer { get; set; }
        public string CustomerName => Customer?.FirstName + " " + Customer?.LastName;
    }
}
