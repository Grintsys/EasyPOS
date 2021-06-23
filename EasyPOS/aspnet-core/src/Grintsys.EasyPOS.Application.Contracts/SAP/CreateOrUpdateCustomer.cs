using System;
using System.Collections.Generic;
using System.Text;

namespace Grintsys.EasyPOS.SAP
{
    public class CreateOrUpdateCustomer
    {
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public string RTN { get; set; }
        public int SalesPersonCode { get; set; }
    }
}
