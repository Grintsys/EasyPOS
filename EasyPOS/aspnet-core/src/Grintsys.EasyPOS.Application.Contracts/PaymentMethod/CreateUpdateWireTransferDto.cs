using System;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdateWireTransferDto
    {
        public string Account { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public float Total { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}