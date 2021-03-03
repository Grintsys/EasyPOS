using Volo.Abp.Modularity;

namespace Grintsys.EasyPOS
{
    [DependsOn(
        typeof(EasyPOSApplicationModule),
        typeof(EasyPOSDomainTestModule)
        )]
    public class EasyPOSApplicationTestModule : AbpModule
    {

    }
}