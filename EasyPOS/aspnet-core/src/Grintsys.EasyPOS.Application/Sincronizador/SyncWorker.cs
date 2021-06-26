using Grintsys.EasyPOS.SAP;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class SyncWorker
        : AsyncPeriodicBackgroundWorkerBase
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
            Logger.LogInformation("Starting: Sync Worker");

            //Resolve dependencies
            var _sapManager = workerContext
                .ServiceProvider
                .GetRequiredService<ISapManager>();

            //Do the work
            await _sapManager.UpsertProducts();
            await _sapManager.UpsertCustomers();

            Logger.LogInformation("Completed: Sync Worker");
        }
    }
}
