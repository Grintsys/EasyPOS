using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodTypeDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
