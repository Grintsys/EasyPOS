using System;
using System.Collections.Generic;
using System.Text;
using Grintsys.EasyPOS.Localization;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS
{
    /* Inherit your application services from this class.
     */
    public abstract class EasyPOSAppService : ApplicationService
    {
        protected EasyPOSAppService()
        {
            LocalizationResource = typeof(EasyPOSResource);
        }
    }
}
