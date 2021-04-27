using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Product
{
    public class ProductAppService :
        CrudAppService<
            Product,
            ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDto
        >,
        IProductAppService
    {
        private readonly IProductRepository _productRepository;
        public ProductAppService(IRepository<Product, Guid> repository, 
            IProductRepository productRepository) : base(repository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetProduct(Guid id, Guid warehouseId)
        {
            var data = await _productRepository.GetAsync(id);
            var dto = ObjectMapper.Map<Product, ProductDto>(data);

            if (warehouseId != Guid.Empty)
            {
                dto.ProductWarehouse = dto.ProductWarehouse.Where(x => x.WarehouseId == warehouseId).ToList();
            }

            return dto;
        }

        public async Task<List<ProductDto>> GetProductList(string filter, Guid warehouseId)
        {
            var products = await _productRepository.GetListAsync();
            var dto = new List<ProductDto>(ObjectMapper.Map<List<Product>, List<ProductDto>>(products));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(filter)
                            || x.Description.ToLower().Contains(filter)
                            || x.Code.ToLower().Contains(filter))
                         .OrderBy(x => x.Name).ToList();
            }

            if (warehouseId != Guid.Empty)
            {
                dto.ForEach(product =>
                {
                    product.ProductWarehouse = product.ProductWarehouse.Where(x => x.WarehouseId == warehouseId).ToList();
                });
                
                dto = dto.Where(x => x.ProductWarehouse.Any()).ToList();
                
                dto.ForEach(product => product.Inventory = product.ProductWarehouse.Sum(x => x.Inventory));
            }

            return dto;
        }

        public async Task<List<ProductLookupDto>> GetProductLookupAsync()
        {
            var products = await _productRepository.GetListAsync();

            return new List<ProductLookupDto>(
                ObjectMapper.Map<List<Product>, List<ProductLookupDto>>(products)
            );
        }

        public async Task<List<ProductDto>> GetProductListByWarehouseAsync(Guid wareHouseId)
        {
            var data = await _productRepository.GetListAsync();

            var dataDto = await MapToGetListOutputDtosAsync(data);
            dataDto = dataDto.Where(x => x.ProductWarehouse.Select(x => x.WarehouseId == wareHouseId).Any()).ToList();

            return dataDto;
        }

    }
}
