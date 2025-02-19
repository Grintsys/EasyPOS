﻿using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.Document
{
    public class Document<T> : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public DocumentState State { get; set; } = DocumentState.Created;
        public ICollection<T> Items { get; set; } = new List<T>();
        public virtual float ISV { get; }
        public virtual float Discount { get; }
        public virtual float SubTotal { get; }
        public virtual float Total { get; }
        public int SalesPersonId { get; set; }
        public string WarehouseCode { get; set; }
        public Guid CustomerId { get; set; }
        public Customer.Customer Customer { get; set; }
        public string CustomerName => Customer?.FullName;
        public string CustomerCode => Customer?.Code;
    }
}
