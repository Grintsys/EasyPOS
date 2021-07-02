using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNoteItem : DocumentItem
    {
        public Guid DebitNoteId { get; set; }
        public DebitNote DebitNote { get; set; }
    }
}
