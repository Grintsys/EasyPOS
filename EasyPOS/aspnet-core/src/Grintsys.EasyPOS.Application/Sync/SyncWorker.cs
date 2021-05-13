using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace Grintsys.EasyPOS.Sync
{
    public class SyncWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public SyncWorker(
            AbpAsyncTimer timer,
            IServiceScopeFactory serviceScopeFactory
            ) : base(
            timer,
            serviceScopeFactory)
        {
            Timer.Period = 600000; //10 minutes
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogInformation("Starting: Getting all the data from SAP");

            var sapManager = workerContext
                .ServiceProvider
                .GetRequiredService<ISapManager>();

            Logger.LogInformation("Working: Pulling products from SAP");

            await sapManager.GetProductListAsync();

            Logger.LogInformation("Working: Pulling customers from SAP");

            await sapManager.GetCustomerListAsync();

            Logger.LogInformation("Completed: Getting products from SAP");
        }
    }
}
