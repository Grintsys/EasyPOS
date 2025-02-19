﻿using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdateCashDto
    {
        public Guid? TenantId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public float Total { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}