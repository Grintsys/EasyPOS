using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grintsys.EasyPOS.Sync
{
    public interface ISapManager
    {
        Task<SapResponse> CreateInvoiceAsync(CreateOrUpdateInvoiceDto input);
        Task<SapResponse> CreateCustomerAsync(CreateOrUpdateCustomer input);
        Task<List<ProductDto>> GetProductListAsync();
        Task<List<CustomerDto>> GetCustomerListAsync();
    }
}
