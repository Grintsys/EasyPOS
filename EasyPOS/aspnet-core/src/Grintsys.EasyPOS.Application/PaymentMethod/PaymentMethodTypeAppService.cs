using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodTypeAppService :
        CrudAppService<
            PaymentMethodType,
            PaymentMethodTypeDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePaymentMethodTypeDto
        >,
        IPaymentMethodTypeAppService
    {
        public PaymentMethodTypeAppService(IRepository<PaymentMethodType, Guid> repository) : base(repository)
        {
        }
    }
}
