using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.ConfigurationManager
{
    public class TenantDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get;  set; }
    }
}
