using System;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public interface IWireTransferAppService :
        ICrudAppService<
            WireTransferDto,
            Guid,
            string,
            CreateUpdateWireTransferDto
        >
    {
    }
}