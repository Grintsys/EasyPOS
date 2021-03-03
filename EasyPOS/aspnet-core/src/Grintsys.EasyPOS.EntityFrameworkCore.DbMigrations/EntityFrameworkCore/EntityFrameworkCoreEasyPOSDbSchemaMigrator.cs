using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Grintsys.EasyPOS.Data;
using Volo.Abp.DependencyInjection;

namespace Grintsys.EasyPOS.EntityFrameworkCore
{
    public class EntityFrameworkCoreEasyPOSDbSchemaMigrator
        : IEasyPOSDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreEasyPOSDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the EasyPOSMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<EasyPOSMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}