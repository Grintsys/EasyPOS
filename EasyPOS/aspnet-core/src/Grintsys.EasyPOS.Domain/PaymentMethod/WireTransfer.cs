using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class WireTransfer : FullAuditedAggregateRoot<Guid>
    {
        public string Account { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public float Total { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}