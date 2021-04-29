using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodType : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
}
