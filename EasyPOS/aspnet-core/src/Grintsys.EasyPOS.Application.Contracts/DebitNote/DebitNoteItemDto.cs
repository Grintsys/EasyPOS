using System;
using Grintsys.EasyPOS.Document;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNoteItemDto : DocumentItemDto
    {
        public Guid DebitNoteId { get; set; }
    }
}
