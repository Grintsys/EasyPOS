﻿using Grintsys.EasyPOS.Document;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.MultiTenancy;

namespace Grintsys.EasyPOS.Order
{
    public class Order : Document<OrderItem>, IMultiTenant
    {
        public OrderType OrderType { get; set; }
        public List<CreditNote.CreditNote> CreditNotes { get; set; } = new List<CreditNote.CreditNote>();
        [CanBeNull] public PaymentMethod.PaymentMethod PaymentMethod { get; set; }
        public override float ISV => Items.Sum(x => x.TaxAmount);
        public override float Discount => Items.Sum(x => (x.Discount /100) * x.SalePrice * x.Quantity);
        public override float SubTotal => Items.Sum(x => x.SalePrice * x.Quantity);
        public override float Total => Items.Sum(x => x.TotalItem);
        public float? PaymentAmount => PaymentMethod?.Amount;
    }
}
