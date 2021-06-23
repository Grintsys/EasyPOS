using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.ConfigurationManager
{
    public class ConfigurationManagerDto : FullAuditedEntityDto<Guid>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
