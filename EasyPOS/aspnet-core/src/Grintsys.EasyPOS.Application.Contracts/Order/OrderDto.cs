using Grintsys.EasyPOS.Document;
using Grintsys.EasyPOS.OrderItem;
using System.Collections.Generic;

namespace Grintsys.EasyPOS.Order
{
    public class OrderDto : DocumentDto<OrderItemDto>
    {
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
