using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class BankCheckAppService : CrudAppService<
            BankCheck,
            BankCheckDto,
            Guid,
            string,
            CreateUpdateBankCheckDto
        >,
        IBankCheckAppService
    {
        public BankCheckAppService(IRepository<BankCheck, Guid> repository) : base(repository)
        {
        }
    }
}