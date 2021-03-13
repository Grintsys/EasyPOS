using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grintsys.EasyPOS.CreateUpdateDtos
{
    public class CreateUpdateOrderDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        public OrderStates OrderState { get; set; } = OrderStates.Created;

        public List<CreateUpdateOrderItemDto> OrderItems { get; set; } = new List<CreateUpdateOrderItemDto>();
    }
}
