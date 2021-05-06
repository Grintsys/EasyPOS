using Grintsys.EasyPOS.CreditNote;
using Grintsys.EasyPOS.DebitNote;
using Grintsys.EasyPOS.Document;
using System.Collections.Generic;
using Grintsys.EasyPOS.PaymentMethod;

namespace Grintsys.EasyPOS.Order
{
    public class OrderDto : DocumentDto<OrderItemDto>
    {
        public OrderType OrderType { get; set; }
        public List<CreditNoteDto> CreditNotes { get; set; } = new List<CreditNoteDto>();
        public PaymentMethodDto PaymentMethod { get; set; }
        public float PaymentAmount { get; set; }
    }
}
