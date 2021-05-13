using System;

namespace Grintsys.EasyPOS.Sync
{
    public class CustomerDto
    {
        public string CardCode { get; set; }
        public string Address { get; set; }
        public string CardName { get; set; }
        public string Currency { get; set; }
        public string U_cedula { get; set; }
        public string U_rtn { get; set; }
        public string LicTradNum { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
