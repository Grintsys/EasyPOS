using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdatePaymentMethodDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PaymentMethodTypeId { get; set; }
        public float Amount { get; set; }
    }
}
