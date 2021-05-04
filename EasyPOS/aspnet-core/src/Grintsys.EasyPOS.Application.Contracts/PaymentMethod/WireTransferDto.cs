using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class WireTransferDto : FullAuditedEntityDto<Guid>
    {
        public string Account { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public float Total { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}