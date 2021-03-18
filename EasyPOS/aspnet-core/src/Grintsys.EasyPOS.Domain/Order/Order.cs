using System;
using System.Collections.Generic;
using System.Linq;
using Grintsys.EasyPOS.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Order
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Guid CustomerId { get; set; }
        public Customer.Customer Customer { get; set; }
        public OrderStates OrderState { get; set; } = OrderStates.Created;
        public ICollection<OrderItem.OrderItem> OrderItems { get; set; } = new List<OrderItem.OrderItem>();
        public float ISV => OrderItems.Sum(x =>  x.Taxes * x.SalePrice * x.Quantity);
        public float Discount => OrderItems.Sum(x => x.Discount * x.SalePrice * x.Quantity);
        public float SubTotal => OrderItems.Sum(x => x.TotalItem);
        public float Total => SubTotal + ISV - Discount;
        public string CustomerName => Customer?.FirstName + " " + Customer?.LastName;
    }
}
