using Grintsys.EasyPOS.Document;
using System.Collections.Generic;
using System.Linq;

namespace Grintsys.EasyPOS.Order
{
    public class Order : Document<OrderItem>
    {
        public List<DebitNote.DebitNote> DebitNotes { get; set; }
        public List<CreditNote.CreditNote> CreditNotes { get; set; }
        public List<PaymentMethod.PaymentMethod> PaymentMethods { get; set; }
        public override float ISV => Items.Sum(x => x.Taxes * x.SalePrice * x.Quantity);
        public override float Discount => Items.Sum(x => x.Discount * x.SalePrice * x.Quantity);
        public override float SubTotal => Items.Sum(x => x.TotalItem);
    }
}
