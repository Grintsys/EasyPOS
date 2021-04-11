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

        public async Task<ListResultDto<ProductLookupDto>> GetProductLookupAsync()
        {
            var products = await _productRepository.GetListAsync();

            return new ListResultDto<ProductLookupDto>(
                ObjectMapper.Map<List<Product>, List<ProductLookupDto>>(products)
            );
        }


        public async Task<List<ProductDto>> GetProductFilteredQueryAsync(string input)
        {
            var products = await _productRepository.GetListAsync();
            var dto = new List<ProductDto>(ObjectMapper.Map<List<Product>, List<ProductDto>>(products));

            if (!input.IsNullOrWhiteSpace())
            {
                input = input.ToLower();
                return dto.WhereIf(!input.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(input) 
                        || x.Description.ToLower().Contains(input) 
                        || x.Code.ToLower().Contains(input))
                    .OrderBy(x => x.Name).ToList();
            }

            return dto;
        }

        public override async Task<ProductDto> GetAsync(Guid id)
        {
            var data = await _productRepository.GetAsync(id);
            var dto = ObjectMapper.Map<Product, ProductDto>(data);
            return dto;
        }

        public async Task<List<ProductDto>> GetProductListAsync()
        {
            var data = await _productRepository.GetListAsync();

            var dataDto = await MapToGetListOutputDtosAsync(data);

            return dataDto;
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
