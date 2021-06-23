using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.ConfigurationManager
{
    public class ConfigurationManager : FullAuditedAggregateRoot<Guid>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
