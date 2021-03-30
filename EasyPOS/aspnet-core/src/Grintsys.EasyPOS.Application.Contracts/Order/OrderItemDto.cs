using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.Order
{
    public class OrderItemDto : DocumentItemDto
    {
        public Guid OrderId { get; set; }
    }
}
