using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class Cash : FullAuditedAggregateRoot<Guid>
    {
        public float Total { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}