using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodDto : FullAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid PaymentMethodTypeId { get; set; }
        public string PaymentMethodTypeName { get; set; }
        public float Amount { get; set; }
    }
}
