using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Product
{
    public class ProductWarehouseAppService :
        CrudAppService<
            ProductWarehouse,
            ProductWarehouseDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductWarehouseDto
        >,
        IProductWarehouseAppService
    {
        private readonly IProductWarehouseRepository _productWarehouseRepository;
        public ProductWarehouseAppService(IRepository<ProductWarehouse, Guid> repository, 
            IProductWarehouseRepository productWarehouseRepository) : base(repository)
        {
            _productWarehouseRepository = productWarehouseRepository;
        }

        public override async Task<ProductWarehouseDto> GetAsync(Guid id)
        {
            var data = await _productWarehouseRepository.GetAsync(id);
            var dto = ObjectMapper.Map<ProductWarehouse, ProductWarehouseDto>(data);
            return dto;
        }

        public override async Task<PagedResultDto<ProductWarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ProductWarehouseDto.Product.Name);
            }

            var data = await _productWarehouseRepository.GetListAsync();

            //data = data
            //    .OrderBy(x => x.GetType().GetProperty(input.Sorting)?.GetValue(x, null))
            //    .Skip(input.SkipCount)
            //    .Take(input.MaxResultCount) as List<DebitNote>;

            var dataDto = await MapToGetListOutputDtosAsync(data);

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<ProductWarehouseDto>(
                totalCount,
                dataDto
            );
        }
    }
}
