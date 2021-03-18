using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Customer
{
    public class CustomerAppService : 
        CrudAppService<
            Customer,
            CustomerDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateCustomerDto
        >,
        ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRepository<EasyPOS.Customer.Customer, Guid> _customerRepository2;
        public CustomerAppService(IRepository<Customer, Guid> repository, 
            ICustomerRepository customerRepository) : base(repository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync()
        {
            var customers = await _customerRepository.GetListAsync();

            return new ListResultDto<CustomerLookupDto>(
                ObjectMapper.Map<List<Customer>, List<CustomerLookupDto>>(customers)
            );
        }
    }
}
