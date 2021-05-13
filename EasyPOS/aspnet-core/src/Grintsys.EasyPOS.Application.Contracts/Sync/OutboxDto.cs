using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Sync
{
    public class OutboxDto : FullAuditedEntityDto<Guid>
    {
        public string MessageType { get; set; }
        public string Body { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsDispatched { get; set; }
    }
}
