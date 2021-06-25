using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Grintsys.EasyPOS.SAP
{
    public interface ISapManager: ITransientDependency
    {
        Task<SapResponse> CreateSalesOrderAsync(CreateOrUpdateSalesOrder input);
        Task<SapResponse> CreateCreditNoteAsync(CreateOrUpdateSalesOrder input);
        Task<SapResponse> CreateDebitNoteAsync(CreateOrUpdateSalesOrder input);
        Task<SapResponse> CreateCustomerAsync(CreateOrUpdateCustomer input);
        Task<List<ProductDto>> GetProductListAsync(int skipCount = 100);
        Task<List<CustomerDto>> GetCustomerListAsync(int skipCount = 100);
        Task<CompanyMetadataDto> GetCompanyMetadata();
    }
}
