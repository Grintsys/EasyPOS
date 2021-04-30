using Grintsys.EasyPOS.Document;
using System.Collections.Generic;
using System.Linq;

namespace Grintsys.EasyPOS.Order
{
    public class Order : Document<OrderItem>
    {
        public OrderType OrderType { get; set; }
        public List<DebitNote.DebitNote> DebitNotes { get; set; } = new List<DebitNote.DebitNote>();
        public List<CreditNote.CreditNote> CreditNotes { get; set; } = new List<CreditNote.CreditNote>();
        public List<PaymentMethod.PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod.PaymentMethod>();
        public override float ISV => Items.Sum(x => x.Taxes * x.SalePrice * x.Quantity);
        public override float Discount => Items.Sum(x => (x.Discount /100) * x.SalePrice * x.Quantity);
        public override float SubTotal => Items.Sum(x => x.SalePrice * x.Quantity);
        public float PaymentAmount => PaymentMethods.Sum(x => x.Amount);
    }
}
