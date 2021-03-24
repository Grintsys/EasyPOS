using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdatePaymentMethod
    {
        public Guid OrderId { get; set; }
        public string Method { get; set; }
        public float Amount { get; set; }
    }
}
