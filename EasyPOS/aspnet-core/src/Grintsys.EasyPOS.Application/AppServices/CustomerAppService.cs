using Grintsys.EasyPOS.CreateUpdateDtos;
using Grintsys.EasyPOS.Dtos;
using Grintsys.EasyPOS.Models;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.AppServices
{
    public class CustomerAppService :
        CrudAppService<
        Customer,
        CustomerDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCustomerDto>, 
        ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, Guid> repository) : base(repository)
        {

        }
    }
}
