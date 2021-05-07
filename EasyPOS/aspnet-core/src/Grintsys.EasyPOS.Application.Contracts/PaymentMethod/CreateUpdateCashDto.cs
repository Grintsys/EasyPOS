using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdateCashDto
    {
        public float Total { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}