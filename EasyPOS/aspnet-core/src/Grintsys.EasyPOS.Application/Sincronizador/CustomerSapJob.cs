using Grintsys.EasyPOS.SAP;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class CustomerSapJob : AsyncBackgroundJob<CustomerSapArgs>, ITransientDependency
    {
        private readonly ISapManager _manager;
        public CustomerSapJob(ISapManager sapManager)
        {
            _manager = sapManager;
        }
        public override async Task ExecuteAsync(CustomerSapArgs args)
        {
            await _manager.CreateCustomerAsync(new CreateOrUpdateCustomer
            {
                CustomerCode = args.CustomerCode,
                CustomerName = args.CustomerName,
                Address = args.Address,
                Cedula = args.Cedula,
                Email = args.Email,
                Phone1 = args.Phone1,
                Phone2 = args.Phone2,
                RTN = args.RTN,
                SalesPersonCode = args.SalesPersonCode
            });
        }
    }
}
