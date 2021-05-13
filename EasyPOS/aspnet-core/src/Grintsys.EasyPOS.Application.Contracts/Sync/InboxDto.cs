using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Sync
{
    public class InboxDto : FullAuditedEntityDto<Guid>
    {
        public string MessageType { get; set; }
        public string Body { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsProcessed { get; set; }
    }
}
