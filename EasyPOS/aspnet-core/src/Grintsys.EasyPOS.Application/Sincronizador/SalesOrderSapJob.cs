using Grintsys.EasyPOS.SAP;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class SalesOrderSapJob 
        : AsyncBackgroundJob<SalesOrderSapArgs>, ITransientDependency
    {
        private readonly ISapManager _manager;
        public SalesOrderSapJob(ISapManager sapManager)
        {
            _manager = sapManager;
        }
        public override async Task ExecuteAsync(SalesOrderSapArgs args)
        {
            await _manager.CreateSalesOrderAsync(new CreateOrUpdateSalesOrder
            {
                CreatedDate = args.CreatedDate,
                CustomerCode = args.CustomerCode,
                CustomerName = args.CustomerName,
                SalesPersonId = args.SalesPersonId,
                WarehouseCode = args.WarehouseCode,
                Lines = args.Lines
            });
        }
    }
}
