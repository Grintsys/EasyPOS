using Grintsys.EasyPOS.SAP;
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
        private readonly ISapManager _sapManager;
        public ProductAppService(IRepository<Product, Guid> repository, 
            IProductRepository productRepository, 
            ISapManager sapManager) : base(repository)
        {
            _productRepository = productRepository;
            _sapManager = sapManager;
        }

        public override Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            input.TenantId = CurrentTenant.Id;
            return base.CreateAsync(input);
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
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), 
                    x => (!string.IsNullOrEmpty(x?.Name) && x.Name.ToLower().Contains(filter))
                    || (!string.IsNullOrEmpty(x?.Description) && x.Description.ToLower().Contains(filter))
                    || (!string.IsNullOrEmpty(x?.Code) && x.Code.ToLower().Contains(filter)))
                    .ToList();
            }

            if (warehouseId != Guid.Empty)
            {
                dto.ForEach(product =>
                {
                    product.ProductWarehouse = product.ProductWarehouse.Where(x => 
                        x.WarehouseId == warehouseId).ToList();
                });
                
                dto = dto.Where(x => x.ProductWarehouse.Any()).ToList();
                
                dto.ForEach(product => 
                        product.Inventory = product.ProductWarehouse.Sum(x => x.Inventory));

                return dto.Where(p => p.Inventory > 0).ToList();
            }

            return dto.OrderBy(x => x.Name).ToList();
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

        public async Task<List<ProductDto>> GetSAPProducts()
        {
            var data = await _sapManager.GetProductListAsync();

            return data.Select(MapProduct).ToList();
        }

        private ProductDto MapProduct(SAP.ProductDto product) => new()
        {
            Code = product.ItemCode,
            Description = product.ItemName,
            Name = product.ItemName,
            SalePrice = (float)product.SalesPrice
        };
    }
}
