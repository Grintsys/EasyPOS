using Grintsys.EasyPOS.SAP;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class SyncService
        : AsyncBackgroundJob<Sincronizador>, ITransientDependency
    {
        private readonly ISapManager _sapManager;
        private readonly IRepository<Sincronizador, Guid> _syncRepository;
        public SyncService(ISapManager sapManager, IRepository<Sincronizador, Guid> paymentMethodRepository)
        {
            _sapManager = sapManager;
            _syncRepository = paymentMethodRepository;
        }

        public override async Task ExecuteAsync(Sincronizador args)
        {
            await _sapManager.UpsertCustomers();
            await _sapManager.UpsertProducts();

        }
    }
}
