using Grintsys.EasyPOS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class EfCorePaymentMethod
        : EfCoreRepository<EasyPOSDbContext, PaymentMethod, Guid>,
            IPaymentMethodRepository
    {
        public EfCorePaymentMethod(IDbContextProvider<EasyPOSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            var data = (await GetQueryableAsync())
                .Include(x => x.PaymentMethodType);
            return await data.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodsByIdAsync(Guid id)
        {
            var data = (await GetQueryableAsync())
                    .Include(x => x.PaymentMethodType)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await data;
        }
    }
}
