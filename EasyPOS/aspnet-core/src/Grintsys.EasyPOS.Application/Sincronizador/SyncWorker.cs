using Grintsys.EasyPOS.SAP;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            Timer.Period = 60000; //10 minutes
        }

        [UnitOfWork]
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
