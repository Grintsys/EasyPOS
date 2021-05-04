using System;
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
    }
}