using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Sync
{
    public class Outbox : FullAuditedAggregateRoot<Guid>
    {
        public string MessageType { get; set; }
        public string Body { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsDispatched { get; set; }
    }
}
