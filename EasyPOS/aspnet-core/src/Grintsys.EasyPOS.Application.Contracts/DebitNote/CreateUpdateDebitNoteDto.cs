using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.DebitNote
{
    public class CreateUpdateDebitNoteDto : CreateUpdateDocumentDto<CreateUpdateDebitNoteItemDto>
    {
        public Guid OrderId { get; set; }
    }
}
