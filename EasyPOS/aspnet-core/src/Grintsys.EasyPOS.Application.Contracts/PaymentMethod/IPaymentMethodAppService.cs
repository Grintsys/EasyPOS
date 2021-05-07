using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public interface IPaymentMethodAppService :
        ICrudAppService<
            PaymentMethodDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePaymentMethodDto
        >
    {
    }
}
