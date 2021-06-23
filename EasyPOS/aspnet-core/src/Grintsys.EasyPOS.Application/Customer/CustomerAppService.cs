using Grintsys.EasyPOS.SAP;
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
        private readonly ISapManager _sapManager;


        public CustomerAppService(IRepository<Customer, Guid> repository, 
            ICustomerRepository customerRepository,
            ISapManager sapManager) : base(repository)
        {
            _customerRepository = customerRepository;
            _sapManager = sapManager;
        }

        public override Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            input.Status = Enums.CustomerStatus.Transferred;
            input.Code = $"c{Guid.NewGuid().ToString().Replace("-", "")}".Truncate(6);
            var customer = base.CreateAsync(input);

            var customerDto = new CreateOrUpdateCustomer
            {
                CustomerCode = customer.Result.Code,
                Address = customer.Result.Address,
                CustomerName = customer.Result.FirstName + " " + customer.Result.LastName,
                RTN = customer.Result.RTN,
                SalesPersonCode = 1,
            };

            _sapManager.CreateCustomerAsync(customerDto);

            return customer;
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
