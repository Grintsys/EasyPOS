using Grintsys.EasyPOS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Grintsys.EasyPOS.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class EasyPOSController : AbpController
    {
        protected EasyPOSController()
        {
            LocalizationResource = typeof(EasyPOSResource);
        }
    }
}