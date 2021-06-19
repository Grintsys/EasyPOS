using System;
using System.Collections.Generic;
using System.Text;

namespace Grintsys.EasyPOS.SAP
{
    public class ProductDto
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItmsGrpNam { get; set; }
        public string WarehouseCode { get; set; }
        public string Stock { get; set; }
        public float SalesPrice { get; set; }
    }
}
