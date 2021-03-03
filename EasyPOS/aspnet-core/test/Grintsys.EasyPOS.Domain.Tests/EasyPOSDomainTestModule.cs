using Grintsys.EasyPOS.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Grintsys.EasyPOS
{
    [DependsOn(
        typeof(EasyPOSEntityFrameworkCoreTestModule)
        )]
    public class EasyPOSDomainTestModule : AbpModule
    {

    }
}