using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<ProductWarehouseDto>> GetProductWarehouseListAsync(string filter)
        {
            var data = await _productWarehouseRepository.GetListAsync();
            
            var dto = await MapToGetListOutputDtosAsync(data);

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), 
                        x => x.ProductName.ToLower().Contains(filter)
                        || x.WarehouseName.ToLower().Contains(filter))
                    .OrderBy(x => x.ProductName).ToList();
            }

            return dto;
        }
        
        public async Task UpdateByProductAndWarehouseId(IEnumerable<CreateUpdateProductWarehouseDto> data)
        {
            foreach (var x in data)
            {
                var dto = await _productWarehouseRepository.GetByProductAndWarehouseAsync(x.ProductId, x.WarehouseId);
                x.Inventory = dto.Inventory - x.Inventory;
                
                await base.UpdateAsync(dto.Id, x);
            }
        }
    }
}
