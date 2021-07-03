using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreateUpdateCreditNoteDto : CreateUpdateDocumentDto<CreateUpdateCreditNoteItemDto>
    {
        public Guid OrderId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
