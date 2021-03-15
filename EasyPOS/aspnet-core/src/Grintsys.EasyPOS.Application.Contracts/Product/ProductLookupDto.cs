using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Product
{
    public class ProductLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
