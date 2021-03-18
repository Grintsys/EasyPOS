using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.OrderItem;

namespace Grintsys.EasyPOS.Order
{
    public class CreateUpdateOrderDto
    {
        public Guid CustomerId { get; set; }

        public OrderStates OrderState { get; set; } = OrderStates.Created;

        public List<CreateUpdateOrderItemDto> OrderItems { get; set; } = new List<CreateUpdateOrderItemDto>();
    }
}
