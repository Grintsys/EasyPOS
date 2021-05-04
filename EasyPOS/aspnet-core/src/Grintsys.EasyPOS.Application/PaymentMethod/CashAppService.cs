using System;
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
    }
}