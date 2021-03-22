using Grintsys.EasyPOS.Document;
using System.Linq;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNote : Document<DebitNoteItem>
    {
        public override float ISV => Items.Sum(x => x.Taxes * x.SalePrice * x.Quantity);
        public override float Discount => Items.Sum(x => x.Discount * x.SalePrice * x.Quantity);
        public override float SubTotal => Items.Sum(x => x.TotalItem);
    }
}
