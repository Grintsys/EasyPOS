using System;
using Grintsys.EasyPOS.Document;

namespace Grintsys.EasyPOS.DebitNote
{
    public class CreateUpdateDebitNoteItemDto : CreateUpdateDocumentItemDto
    {
        public Guid DebitNoteId { get; set; }
    }
}
