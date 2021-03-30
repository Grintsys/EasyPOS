using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public interface IPaymentMethodRepository : IRepository<PaymentMethod, Guid>
    {
        Task<List<PaymentMethod>> GetPaymentMethodsAsync();
        Task<PaymentMethod> GetPaymentMethodsByIdAsync(Guid id);
    }
}
