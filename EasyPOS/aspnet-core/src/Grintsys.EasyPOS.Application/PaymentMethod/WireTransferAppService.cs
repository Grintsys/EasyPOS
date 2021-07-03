using System;
using System.Threading.Tasks;
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

        public override Task<WireTransferDto> CreateAsync(CreateUpdateWireTransferDto input)
        {
            input.TenantId = CurrentTenant.Id;
            return base.CreateAsync(input);
        }
    }
}