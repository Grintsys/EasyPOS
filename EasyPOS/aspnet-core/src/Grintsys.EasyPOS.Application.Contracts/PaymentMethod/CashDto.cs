using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CashDto : FullAuditedEntityDto<Guid>
    {
        public float Total { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}