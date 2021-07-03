using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CashAppService: CrudAppService<
            Cash,
            CashDto,
            Guid,
            string,
            CreateUpdateCashDto
        >,
        ICashAppService
    {
        public CashAppService(IRepository<Cash, Guid> repository) : base(repository)
        {
        }

        public override Task<CashDto> CreateAsync(CreateUpdateCashDto input)
        {
            input.TenantId = CurrentTenant.Id;
            return base.CreateAsync(input);
        }
    }
}