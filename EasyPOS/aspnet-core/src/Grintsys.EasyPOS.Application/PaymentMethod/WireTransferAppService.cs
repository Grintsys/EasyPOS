using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class WireTransferAppService : CrudAppService<
            WireTransfer,
            WireTransferDto,
            Guid,
            string,
            CreateUpdateWireTransferDto
        >,
        IWireTransferAppService
    {
        public WireTransferAppService(IRepository<WireTransfer, Guid> repository) : base(repository)
        {
        }
    }
}