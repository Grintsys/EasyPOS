using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.Order
{
    public class OrderItem : DocumentItem
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
