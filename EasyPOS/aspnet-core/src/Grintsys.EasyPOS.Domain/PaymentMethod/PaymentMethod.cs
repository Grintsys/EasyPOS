using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethod : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid OrderId { get; set; }
        public Order.Order Order { get; set; }
        [CanBeNull] public CreditDebitCard CreditDebitCard { get; set; }
        [CanBeNull] public Cash Cash { get; set; }
        [CanBeNull] public WireTransfer WireTransfer { get; set; }
        public List<BankCheck> BankChecks { get; set; } = new List<BankCheck>();
        public float? Amount => Cash?.Total + WireTransfer?.Total + CreditDebitCard?.Total + BankChecks.Sum(x => x.Total);
    }
}
