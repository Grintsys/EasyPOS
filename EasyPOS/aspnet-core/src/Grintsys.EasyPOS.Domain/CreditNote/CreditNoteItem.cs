using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.CreditNote
{
    public class CreditNoteItem : DocumentItem
    {
        public Guid CreditNoteId { get; set; }
        public CreditNote CreditNote { get; set; }
    }
}
