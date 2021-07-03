using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdateCreditDebitCardDto
    {
        public Guid? TenantId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public float Total { get; set; }
        public string Name { get; set; }
        public DateTime ValidThru { get; set; }
        public string PersonId { get; set; }
        public string CertificateRetentionNumber { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}