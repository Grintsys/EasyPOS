using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethod : FullAuditedAggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }
        public Order.Order Order { get; set; }
        public Guid? CreditDebitCardId { get; set; }
        [CanBeNull] public CreditDebitCard CreditDebitCard { get; set; }
        public Guid? CashId { get; set; }
        [CanBeNull] public Cash Cash { get; set; }
        public Guid? WireTransferId { get; set; }
        [CanBeNull] public WireTransfer WireTransfer { get; set; }
        public List<BankCheck> BankChecks { get; set; } = new List<BankCheck>();
        public float? Amount => Cash?.Total + WireTransfer?.Total + CreditDebitCard?.Total + BankChecks.Sum(x => x.Total);

    }
}
