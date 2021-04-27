using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Customer
{
    public interface ICustomerAppService :
        ICrudAppService<
            CustomerDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCustomerDto>
    {
        Task<List<CustomerDto>> GetCustomerList(string filter);
    }
}
