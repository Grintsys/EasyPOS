using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreateUpdateCreditNoteItemDto : CreateUpdateDocumentItemDto
    {
        public Guid CreditNoteId { get; set; }
    }
}
