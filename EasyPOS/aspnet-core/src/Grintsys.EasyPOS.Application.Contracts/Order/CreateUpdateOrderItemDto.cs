using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.OrderItem
{
    public class CreateUpdateOrderItemDto : CreateUpdateDocumentItemDto
    {
        public Guid OrderId { get; set; }
    }
}
