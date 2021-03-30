using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNoteItemDto : DocumentItemDto
    {
        public Guid CreditNoteId { get; set; }
    }
}
