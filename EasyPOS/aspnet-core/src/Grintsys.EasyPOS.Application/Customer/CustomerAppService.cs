using System;
using System.Collections.Generic;
using System.Linq;
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

        public CustomerAppService(IRepository<Customer, Guid> repository, 
            ICustomerRepository customerRepository) : base(repository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDto>> GetCustomerList(string filter)
        {
            var customers = await _customerRepository.GetListAsync();
            var dto = new List<CustomerDto>(ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), 
                    x => x.FirstName.ToLower().Contains(filter) 
                    || x.LastName.ToLower().Contains(filter)
                    || x.FullName.ToLower().Contains(filter)
                    || x.RTN.ToLower().Contains(filter)
                    || x.IdNumber.ToLower().Contains(filter)
                    || x.PhoneNumber.ToLower().Contains(filter)
                    || x.Address.ToLower().Contains(filter)
                    || x.Code.ToLower().Contains(filter))
                    .OrderBy(x => x.FirstName).ToList();
            }

            return dto;
        }
    }
}
