using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class EfCorePaymentMethodItemRepository
        : EfCoreRepository<EasyPOSDbContext, PaymentMethodType, Guid>,
            IPaymentMethodTypeRepository
    {
        public EfCorePaymentMethodItemRepository(IDbContextProvider<EasyPOSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<PaymentMethodType>> GetPaymentMethodTypes()
        {
            var data = (await GetQueryableAsync());
            return await data.ToListAsync();
        }
    }
}