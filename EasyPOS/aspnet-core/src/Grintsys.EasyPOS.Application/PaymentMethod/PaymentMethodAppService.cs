using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodAppService : CrudAppService<
            PaymentMethod,
            PaymentMethodDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePaymentMethodDto
        >,
        IPaymentMethodAppService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodAppService(IRepository<PaymentMethod, Guid> repository, 
            IPaymentMethodRepository paymentMethodRepository) : base(repository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public override async Task<PaymentMethodDto> GetAsync(Guid id)
        {
            var paymentMethod = await _paymentMethodRepository.GetPaymentMethodsByIdAsync(id);
            var dto = ObjectMapper.Map<PaymentMethod, PaymentMethodDto>(paymentMethod);
            return dto;
        }

        public async Task<List<PaymentMethodDto>> GetListPaymentMethodsAsync(string filter)
        {
            var paymentMethods = await _paymentMethodRepository.GetPaymentMethodsAsync();
            var dto = await MapToGetListOutputDtosAsync(paymentMethods);

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), 
                        x => x.PaymentMethodTypeName.ToLower().Contains(filter))
                    .OrderBy(x => x.PaymentMethodTypeName).ToList();
            }

            return dto;
        }
    }
}
