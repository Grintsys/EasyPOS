﻿using Grintsys.EasyPOS.Enums;
using System;
using System.Collections.Generic;

namespace Grintsys.EasyPOS.Document
{
    public class CreateUpdateDocumentDto<T> 
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DocumentState State { get; set; } = DocumentState.Created;
        public List<T> Items { get; set; } = new List<T>();
    }
}
