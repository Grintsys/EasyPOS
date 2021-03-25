using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdatePaymentMethodDto
    {
        public Guid OrderId { get; set; }
        public string Method { get; set; }
        public float Amount { get; set; }
    }
}
