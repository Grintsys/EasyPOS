using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdateBankCheckDto
    {
        public Guid? TenantId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public float Total { get; set; }
        public string Bank { get; set; }
        public DateTime Date { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}