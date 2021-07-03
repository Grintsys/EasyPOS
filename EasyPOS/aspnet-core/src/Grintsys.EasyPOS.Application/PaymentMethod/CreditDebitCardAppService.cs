using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreditDebitCardAppService: CrudAppService<
            CreditDebitCard,
            CreditDebitCardDto,
            Guid,
            string,
            CreateUpdateCreditDebitCardDto
        >,
        ICreditDebitCardAppService
    {
        public CreditDebitCardAppService(IRepository<CreditDebitCard, Guid> repository) : base(repository)
        {
        }

        public override Task<CreditDebitCardDto> CreateAsync(CreateUpdateCreditDebitCardDto input)
        {
            input.TenantId = CurrentTenant.Id;
            return base.CreateAsync(input);
        }
    }
}