using System;
using System.Collections.Generic;
using Grintsys.EasyPOS.CreditNote;
using Grintsys.EasyPOS.DebitNote;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Order
{
    public class OrderDocumentDto : FullAuditedEntityDto<Guid>
    {
        public List<DebitNoteDto> DebitNotes { get; set; } = new List<DebitNoteDto>();
        public List<CreditNoteDto> CreditNotes { get; set; } = new List<CreditNoteDto>();
    }
}