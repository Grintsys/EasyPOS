using System;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public interface ICashAppService:
        ICrudAppService<
            CashDto,
            Guid,
            string,
            CreateUpdateCashDto
        >
    {
        
    }
}