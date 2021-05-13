using System;
using System.Collections.Generic;

namespace Grintsys.EasyPOS.Sync
{
    public class OrderChangedEvent
    {
        public Guid OutboxId { get; set; }
        public List<Guid> OrderList { get; set; }
    }
}
