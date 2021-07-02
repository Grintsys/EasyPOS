using Grintsys.EasyPOS.Document;
using System;
using System.Linq;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNote : Document<CreditNoteItem>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid OrderId { get; set; }
        public Order.Order Order { get; set; }
        public override float ISV => Items.Sum(x => x.TaxAmount * x.SalePrice * x.Quantity);
        public override float Discount => Items.Sum(x => (x.Discount / 100) * x.SalePrice * x.Quantity);
        public override float SubTotal => Items.Sum(x => x.SalePrice * x.Quantity);
        public override float Total => Items.Sum(x => x.TotalItem);
    }
}
