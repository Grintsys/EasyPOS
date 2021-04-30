using Grintsys.EasyPOS.Document;
using System;
using System.Linq;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNote : Document<DebitNoteItem>
    {
        public Guid OrderId { get; set; }
        public Order.Order Order { get; set; }
        public override float ISV => Items.Sum(x => x.Taxes * x.SalePrice * x.Quantity);
        public override float Discount => Items.Sum(x => (x.Discount / 100) * x.SalePrice * x.Quantity);
        public override float SubTotal => Items.Sum(x => x.SalePrice * x.Quantity);
    }
}
