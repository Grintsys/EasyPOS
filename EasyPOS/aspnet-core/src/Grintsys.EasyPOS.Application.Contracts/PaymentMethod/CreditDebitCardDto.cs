using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreditDebitCardDto : FullAuditedEntityDto<Guid>
    {
        public float Total { get; set; }
        public string Name { get; set; }
        public DateTime ValidThru { get; set; }
        public string PersonId { get; set; }
        public string CertificateRetentionNumber { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}