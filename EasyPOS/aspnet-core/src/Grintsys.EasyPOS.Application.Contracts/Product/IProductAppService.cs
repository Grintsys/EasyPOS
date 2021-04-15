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
        Task<ProductDto> GetProduct(Guid id);
        Task<List<ProductDto>> GetProductList(string filter);
        Task<List<ProductDto>> GetProductListByWarehouseAsync(Guid wareHouseId);
    }
}
