﻿using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class BankCheck : FullAuditedAggregateRoot<Guid>
    {
        public float Total { get; set; }
        public string Bank { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}