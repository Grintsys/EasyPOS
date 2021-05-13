namespace Grintsys.EasyPOS.Sync
{
    public class CreateOrUpdateCustomer
    {
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public string RTN { get; set; }
        public int SAlesPersonCode { get; set; }
    }
}
