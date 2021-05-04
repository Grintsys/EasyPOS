using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class BankCheckDto : FullAuditedEntityDto<Guid>
    {
        public float Total { get; set; }
        public string Bank { get; set; }
        public DateTime Date { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}