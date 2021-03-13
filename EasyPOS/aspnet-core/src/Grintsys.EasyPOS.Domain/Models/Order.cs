using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Models
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public OrderStates OrderState { get; set; } = OrderStates.Created;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public float ISV => OrderItems.Sum(x =>  x.Taxes * x.SalePrice * x.Quantity);
        public float SubTotal => OrderItems.Sum(x => x.TotalItem);
        public float Total => SubTotal + ISV;
    }
}
