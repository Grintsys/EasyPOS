using Grintsys.EasyPOS.CreditNote;
using Grintsys.EasyPOS.DebitNote;
using Grintsys.EasyPOS.Document;
using System.Collections.Generic;
using Grintsys.EasyPOS.PaymentMethod;

namespace Grintsys.EasyPOS.Order
{
    public class OrderDto : DocumentDto<OrderItemDto>
    {
        public List<DebitNoteDto> DebitNotes { get; set; } = new List<DebitNoteDto>();
        public List<CreditNoteDto> CreditNotes { get; set; } = new List<CreditNoteDto>();
        public List<PaymentMethodDto> PaymentMethods { get; set; } = new List<PaymentMethodDto>();
    }
}
