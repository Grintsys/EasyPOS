using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Grintsys.EasyPOS.SAP
{
    public interface ISapManager: ITransientDependency
    {
        Task CreateInvoiceAsync(CreateOrUpdateSalesOrder input);
        Task CreateSalesOrderAsync(CreateOrUpdateSalesOrder input);
        Task CreateCreditNoteAsync(CreateOrUpdateSalesOrder input);
        Task CreateDebitNoteAsync(CreateOrUpdateSalesOrder input);
        Task CreateCustomerAsync(CreateOrUpdateCustomer input);
        Task<List<ProductDto>> GetProductListAsync(int skipCount = 100);
        Task<List<CustomerDto>> GetCustomerListAsync(int skipCount = 100);
        Task<CompanyMetadataDto> GetCompanyMetadata();
        Task UpsertProducts();
        Task UpsertCustomers();
    }
}
