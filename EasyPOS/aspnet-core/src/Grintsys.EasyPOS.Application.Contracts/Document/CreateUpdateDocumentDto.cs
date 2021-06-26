using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;

namespace Grintsys.EasyPOS.Document
{
    public class CreateUpdateDocumentDto<T> 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int SalesPersonId { get; set; }
        public string WarehouseCode { get; set; }
        public DocumentState State { get; set; } = DocumentState.Created;
        public List<T> Items { get; set; } = new List<T>();
    }
}
