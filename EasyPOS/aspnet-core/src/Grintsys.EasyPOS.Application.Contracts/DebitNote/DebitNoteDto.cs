﻿using System;
using Grintsys.EasyPOS.Document;

namespace Grintsys.EasyPOS.DebitNote
{
    public class DebitNoteDto : DocumentDto<DebitNoteItemDto>
    {
        public Guid DebitNoteId { get; set; }
    }
}
