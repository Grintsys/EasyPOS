using Grintsys.EasyPOS.Document;
using System;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNoteItem : DocumentItem, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid CreditNoteId { get; set; }
        public CreditNote CreditNote { get; set; }
    }
}
