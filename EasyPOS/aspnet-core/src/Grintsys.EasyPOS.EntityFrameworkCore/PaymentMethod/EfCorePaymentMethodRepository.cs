using Grintsys.EasyPOS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class EfCorePaymentMethodRepository
        : EfCoreRepository<EasyPOSDbContext, PaymentMethod, Guid>,
            IPaymentMethodRepository
    {
        public EfCorePaymentMethodRepository(IDbContextProvider<EasyPOSDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            var data = (await GetQueryableAsync());
            return await data.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodsByIdAsync(Guid id)
        {
            var data = (await GetQueryableAsync())
                .FirstOrDefaultAsync(x => x.Id == id);
            return await data;
        }
    }
}
