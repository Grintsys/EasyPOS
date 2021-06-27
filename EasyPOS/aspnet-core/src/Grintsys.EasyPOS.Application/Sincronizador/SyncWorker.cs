using Grintsys.EasyPOS.SAP;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class SyncWorker
        : AsyncPeriodicBackgroundWorkerBase, ISingletonDependency
    {

        public SyncWorker(
            AbpAsyncTimer timer,
            IServiceScopeFactory serviceScopeFactory
        ) : base(
            timer,
            serviceScopeFactory)
        {
            Timer.Period = 60000; //1 minutes
        }

        [UnitOfWork]
        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            var startTime = DateTime.UtcNow;
            Logger.LogInformation($"Starting: Sync Worker, {startTime}");

            //Resolve dependencies
            var _sapManager = workerContext
                .ServiceProvider
                .GetRequiredService<ISapManager>();

            //Do the work
            await _sapManager.UpsertProducts();
            await _sapManager.UpsertCustomers();

            var endTime = DateTime.UtcNow;
            Logger.LogInformation($"Completed: Sync Worker, {endTime}");
        }
    }
}
