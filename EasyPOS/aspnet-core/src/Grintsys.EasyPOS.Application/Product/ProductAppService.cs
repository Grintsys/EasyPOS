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

            var dto = new List<ProductDto>(
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products))
                .WhereIf(!input.IsNullOrWhiteSpace(), x=> x.Name.Contains(input)
                || x.Description.Contains(input) || x.Code.Contains(input))
                .OrderBy(x => x.Name).ToList();

            return dto;
        }
    }
}
