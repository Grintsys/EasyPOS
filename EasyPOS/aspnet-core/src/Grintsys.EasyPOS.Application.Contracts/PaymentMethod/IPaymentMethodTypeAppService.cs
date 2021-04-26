using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public interface IPaymentMethodTypeAppService :
        ICrudAppService<
            PaymentMethodTypeDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePaymentMethodTypeDto
        >
    {
        Task<List<PaymentMethodTypeDto>> GetListPaymentMethods();
    }
}
