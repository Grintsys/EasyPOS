using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Grintsys.EasyPOS
{
    [Dependency(ReplaceServices = true)]
    public class EasyPOSBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "EasyPOS";
    }
}
