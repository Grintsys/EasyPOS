using Grintsys.EasyPOS.Document;
using System;

namespace Grintsys.EasyPOS.Order
{
    public class OrderItem : DocumentItem
    {
        public Guid OrderId { get; set; }
        public EasyPOS.Order.Order Order { get; set; }
    }
}
