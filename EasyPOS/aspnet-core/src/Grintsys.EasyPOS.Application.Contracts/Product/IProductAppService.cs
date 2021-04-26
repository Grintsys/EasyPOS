using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Product
{
    public interface IProductAppService :
        ICrudAppService<
            ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDto>
    {
        Task<List<ProductLookupDto>> GetProductLookupAsync();
        Task<ProductDto> GetProduct(Guid id, Guid wareHouseId);
        Task<List<ProductDto>> GetProductList(string filter, Guid wareHouseId);
        Task<List<ProductDto>> GetProductListByWarehouseAsync(Guid wareHouseId);
    }
}
