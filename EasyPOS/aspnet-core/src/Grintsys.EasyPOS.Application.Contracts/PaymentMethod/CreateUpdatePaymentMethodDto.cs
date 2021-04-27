using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdatePaymentMethodDto
    {
        public Guid PaymentMethodTypeId { get; set; }
        public float Amount { get; set; }
    }
}
