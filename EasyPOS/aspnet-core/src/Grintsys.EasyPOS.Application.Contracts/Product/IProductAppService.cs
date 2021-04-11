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
        Task<ListResultDto<ProductLookupDto>> GetProductLookupAsync();
        Task<List<ProductDto>> GetProductFilteredQueryAsync(string input);
        Task<List<ProductDto>> GetProductListAsync();
        Task<List<ProductDto>> GetProductListByWarehouseAsync(Guid wareHouseId);
    }
}
