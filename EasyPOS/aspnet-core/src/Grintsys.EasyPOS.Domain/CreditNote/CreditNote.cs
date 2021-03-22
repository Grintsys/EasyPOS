using System.Linq;
using Grintsys.EasyPOS.Document;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNote : Document<CreditNoteItem>
    {
        public override float ISV => Items.Sum(x => x.Taxes * x.SalePrice * x.Quantity);
        public override float Discount => Items.Sum(x => x.Discount * x.SalePrice * x.Quantity);
        public override float SubTotal => Items.Sum(x => x.TotalItem);
    }
}
