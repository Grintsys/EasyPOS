using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.Document
{
    public class DocumentItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public float SalePrice { get; set; }
        public bool Taxes { get; set; }
        public float TaxAmount { get; set; }
        public string SelectedTax { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public float TotalItem { get; set; }
    }
}
