using System;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public interface IBankCheckAppService :
        ICrudAppService<
            BankCheckDto,
            Guid,
            string,
            CreateUpdateBankCheckDto
        >
    {
        
    }
}