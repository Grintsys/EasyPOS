using System.Collections.Generic;
using Grintsys.EasyPOS.Document;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNoteDto : DocumentDto<CreditNoteItemDto>
    {
        public List<CreditNoteItemDto> CreditNoteItems { get; set; } = new List<CreditNoteItemDto>();
    }
}
