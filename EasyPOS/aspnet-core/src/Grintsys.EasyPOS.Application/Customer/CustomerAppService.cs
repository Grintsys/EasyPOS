using Grintsys.EasyPOS.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
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
        private readonly IRepository<Customer, Guid> _repository;
        private readonly ISapManager _sapManager;
        private readonly IBackgroundJobManager _backgroundJobManager;


        public CustomerAppService(IRepository<Customer, Guid> repository, 
            ICustomerRepository customerRepository,
            ISapManager sapManager,
            IBackgroundJobManager backgroundJobManager) : base(repository)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _sapManager = sapManager;
            _backgroundJobManager = backgroundJobManager;
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
                CustomerName = customer.Result.FullName,
                RTN = customer.Result.RTN,
                SalesPersonCode = 1,
                Cedula = customer.Result.IdNumber
            };

            _backgroundJobManager.EnqueueAsync(_sapManager.CreateCustomerAsync(customerDto));            

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
                    x => x.FullName.ToLower().Contains(filter) 
                    || (!string.IsNullOrEmpty(x.RTN) && x.RTN.ToLower().Contains(filter))
                    || (!string.IsNullOrEmpty(x.IdNumber) && x.IdNumber.ToLower().Contains(filter))
                    || (!string.IsNullOrEmpty(x.PhoneNumber) && x.PhoneNumber.ToLower().Contains(filter))
                    || (!string.IsNullOrEmpty(x.Address) && x.Address.ToLower().Contains(filter))
                    || (!string.IsNullOrEmpty(x.Code) && x.Code.ToLower().Contains(filter)))
                    .OrderBy(x => x.FullName).ToList();
            }

            return dto;
        }

        public async Task<object> GetNextCode()
        {
            var customersList = await _repository.GetListAsync();

            var suffix = customersList.Any() 
                ? customersList.OrderByDescending(x => x.Suffix).FirstOrDefault().Suffix + 1
                : 1;

            string asString = suffix.ToString("D" + 5);

            return new { Code = "CEP" + asString };
        }
    }
}
