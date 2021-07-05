using Grintsys.EasyPOS.Order;
using System;
using System.Collections.Generic;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class CreaditNodeSapArgs
    {
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int SalesPersonId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string WarehouseCode { get; set; }
        public List<OrderItemDto> Lines { get; set; }
    }
}
