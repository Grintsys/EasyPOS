using Grintsys.EasyPOS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Grintsys.EasyPOS.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(EasyPOSEntityFrameworkCoreDbMigrationsModule),
        typeof(EasyPOSApplicationContractsModule)
        )]
    public class EasyPOSDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
