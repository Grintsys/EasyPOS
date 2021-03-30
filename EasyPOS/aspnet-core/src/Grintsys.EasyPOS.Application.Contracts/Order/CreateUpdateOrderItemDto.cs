using System;
using Grintsys.EasyPOS.Document;

namespace Grintsys.EasyPOS.Order
{
    public class CreateUpdateOrderItemDto : CreateUpdateDocumentItemDto
    {
        public Guid OrderId { get; set; }
    }
}
