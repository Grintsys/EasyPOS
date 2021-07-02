using Grintsys.EasyPOS.Document;
using System;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.Order
{
    public class OrderItem : DocumentItem, IMultiTenant
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
