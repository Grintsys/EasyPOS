using System;
using System.Threading.Tasks;
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

        public override Task<BankCheckDto> CreateAsync(CreateUpdateBankCheckDto input)
        {
            input.TenantId = CurrentTenant.Id;
            return base.CreateAsync(input);
        }
    }
}