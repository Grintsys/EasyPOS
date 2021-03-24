using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS
{
    public class PaymentMethodDto : FullAuditedEntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public string Method { get; set; }
        public float Amount { get; set; }
    }
}
