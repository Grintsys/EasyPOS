using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Product
{
    public class WarehouseAppService :
        CrudAppService<
            Warehouse,
            WarehouseDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateWarehouseDto
        >,
        IWarehouseAppService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseAppService(IRepository<Warehouse, Guid> repository, IWarehouseRepository warehouseRepository) : base(repository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public override async Task<WarehouseDto> GetAsync(Guid id)
        {
            var data = await _warehouseRepository.GetAsync(id);
            var dto = ObjectMapper.Map<Warehouse, WarehouseDto>(data);
            return dto;
        }

        public async Task<List<WarehouseDto>> GetWarehouseList(string filter)
        {
            var data = await _warehouseRepository.GetWarehousesAsync();
            try
            {
                var dto = new List<WarehouseDto>(ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(data));
                if (!filter.IsNullOrWhiteSpace())
                {
                    filter = filter.ToLower();
                    dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), 
                            x => x.Name.ToLower().Contains(filter) ||
                                 x.Address.ToLower().Contains(filter))
                        .OrderBy(x => x.Name).ToList();
                }
                return dto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}
