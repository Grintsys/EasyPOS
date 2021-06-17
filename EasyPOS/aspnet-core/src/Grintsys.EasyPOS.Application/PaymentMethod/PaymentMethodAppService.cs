using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class PaymentMethodAppService : 
        CrudAppService<
            PaymentMethod,
            PaymentMethodDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePaymentMethodDto
        >,
        IPaymentMethodAppService
    {

        public PaymentMethodAppService(IRepository<PaymentMethod, Guid> repository) : base(repository)
        {
        }
    }
}
