using System.Collections.Generic;
using Grintsys.EasyPOS.Document;
using Grintsys.EasyPOS.PaymentMethod;

namespace Grintsys.EasyPOS.Order
{
    public class CreateUpdateOrderDto : CreateUpdateDocumentDto<CreateUpdateOrderItemDto>
    {
        public List<CreateUpdatePaymentMethodDto> PaymentMethods { get; set; } =
            new List<CreateUpdatePaymentMethodDto>();
    }
}
