using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public interface IPaymentMethodTypeRepository : IRepository<PaymentMethodType, Guid>
    {
        Task<List<PaymentMethodType>> GetPaymentMethodTypes();
    }
}