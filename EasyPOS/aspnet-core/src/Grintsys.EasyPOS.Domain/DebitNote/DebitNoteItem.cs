using Grintsys.EasyPOS.Document;
using System;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNoteItem : DocumentItem, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid DebitNoteId { get; set; }
        public DebitNote DebitNote { get; set; }
    }
}
