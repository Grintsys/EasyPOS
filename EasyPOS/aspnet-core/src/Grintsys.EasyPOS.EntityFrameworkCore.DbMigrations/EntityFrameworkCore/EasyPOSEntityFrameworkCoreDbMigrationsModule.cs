using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Grintsys.EasyPOS.EntityFrameworkCore
{
    [DependsOn(
        typeof(EasyPOSEntityFrameworkCoreModule)
        )]
    public class EasyPOSEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EasyPOSMigrationsDbContext>();
        }
    }
}
