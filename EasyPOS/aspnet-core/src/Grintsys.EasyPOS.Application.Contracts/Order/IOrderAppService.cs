using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Order
{
    public interface IOrderAppService :
        ICrudAppService<
            OrderDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderDto
        >
    {
        Task<List<OrderDto>> GetOrderList(string filter);
        Task<OrderDocumentDto> GetOrderDocuments(Guid orderId);
    }
}
