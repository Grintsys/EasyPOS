using System;

namespace Grintsys.EasyPOS.SAP
{
    public class CustomerDto
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public char CartType { get; set; }
        public string RTN { get; set; }
        public string Cedula { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
