﻿using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodDto : FullAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid? CreditDebitCardId { get; set; }
        public Guid? CashId { get; set; }
        public Guid? WireTransferId { get; set; }
        public List<BankCheckDto> BankChecks { get; set; } = new List<BankCheckDto>();
        public float Amount { get; set; }
    }
}
