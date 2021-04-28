using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Document
{
    public class DocumentItem : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public float SalePrice { get; set; }
        public float Taxes { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public float TotalItem => Quantity * SalePrice + (SalePrice * Taxes * Quantity) - (SalePrice * (Discount / 100) * Quantity);
    }
}
