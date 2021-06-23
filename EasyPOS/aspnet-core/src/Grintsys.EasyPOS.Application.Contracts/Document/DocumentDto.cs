using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Document
{
    public class DocumentDto<T> : FullAuditedEntityDto<Guid>
    {
        public int SalesPersonId { get; set; }
        public string WarehouseCode { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public DocumentState State { get; set; }
        public float SubTotal { get; set; }
        public float ISV { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
