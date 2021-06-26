using System.Collections.Generic;

namespace Grintsys.EasyPOS.SAP
{
    public class CompanyMetadataDto
    {
        public string Currency { get; set; }
        public List<TaxDto> Taxes { get; set; }
        public List<BankDto> Banks { get; set; }
    }
}
