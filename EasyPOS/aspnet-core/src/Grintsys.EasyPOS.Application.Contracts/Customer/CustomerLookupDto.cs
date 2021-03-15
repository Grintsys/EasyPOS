using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Customer
{
    public class CustomerLookupDto : EntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
    }
}
