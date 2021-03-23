using Grintsys.EasyPOS.Enums;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethod : FullAuditedAggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }
        public Order.Order Order { get; set; }
        public PaymentMethods Method { get; set; }
        public float Amount { get; set; }
    }
}
