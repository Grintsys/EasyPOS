using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodTypeDto : FullAuditedEntityDto<Guid>
    {
        public string Method { get; set; }
    }
}
