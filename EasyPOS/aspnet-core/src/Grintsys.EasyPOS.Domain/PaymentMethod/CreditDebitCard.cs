using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod 
{
    public class CreditDebitCard : FullAuditedAggregateRoot<Guid>
    {
        public float Total { get; set; }
        public string Name { get; set; }
        public DateTime ValidThru { get; set; }
        public string PersonId { get; set; }
        public string CertificateRetentionNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}