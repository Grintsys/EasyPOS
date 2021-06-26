namespace Grintsys.EasyPOS.SAP
{
    public class ProductDto
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string WhsCode { get; set; }
        public double OnHand { get; set; }
        public double SalesPrice { get; set; }
        public bool HasTaxes { get; set; }
    }
}
