using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNoteDto : DocumentDto<CreditNoteItemDto>
    {
        public Guid OrderId { get; set; }
    }
}
