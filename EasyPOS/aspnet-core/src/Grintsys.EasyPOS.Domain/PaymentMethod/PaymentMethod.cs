using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethod : FullAuditedAggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }
        public Order.Order Order { get; set; }
        public Guid PaymentMethodTypeId { get; set; }
        public PaymentMethodType PaymentMethodType { get; set; }
        public string PaymentMethodTypeName => PaymentMethodType?.Name ?? string.Empty;
        public float Amount { get; set; }
    }
}
