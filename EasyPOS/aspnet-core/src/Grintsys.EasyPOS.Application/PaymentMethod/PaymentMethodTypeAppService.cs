using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<PaymentMethodTypeDto>> GetListPaymentMethods()
        {
            var x = await base.GetListAsync(new PagedAndSortedResultRequestDto());
            return x.Items.ToList();
        }
    }
}
