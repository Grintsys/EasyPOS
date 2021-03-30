using System;
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

        public override async Task<PagedResultDto<PaymentMethodDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(PaymentMethod.PaymentMethodTypeName);
            }

            var paymentMethods = await _paymentMethodRepository.GetPaymentMethodsAsync();

            //paymentMethods = paymentMethods
            //    .OrderBy(x => x.GetType().GetProperty(input.Sorting)?.GetValue(x, null))
            //    .Skip(input.SkipCount)
            //    .Take(input.MaxResultCount) as List<DebitNote>;

            var paymentMethodDto = await MapToGetListOutputDtosAsync(paymentMethods);

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<PaymentMethodDto>(
                totalCount,
                paymentMethodDto
            );
        }
    }
}
