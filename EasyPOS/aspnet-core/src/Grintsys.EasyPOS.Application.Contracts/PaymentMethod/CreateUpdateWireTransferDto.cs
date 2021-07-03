using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdateWireTransferDto
    {
        public Guid? TenantId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Account { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public float Total { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}