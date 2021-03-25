using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdatePaymentMethodDto
    {
        public Guid OrderId { get; set; }
        public Guid PaymentMethodTypeId { get; set; }
        public float Amount { get; set; }
    }
}
