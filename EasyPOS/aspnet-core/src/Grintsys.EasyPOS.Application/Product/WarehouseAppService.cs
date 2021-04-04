using System;
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

        public override async Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(WarehouseDto.Name);
            }

            var data = await _warehouseRepository.GetListAsync();

            //data = data
            //    .OrderBy(x => x.GetType().GetProperty(input.Sorting)?.GetValue(x, null))
            //    .Skip(input.SkipCount)
            //    .Take(input.MaxResultCount) as List<DebitNote>;

            var dataDto = await MapToGetListOutputDtosAsync(data);

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<WarehouseDto>(
                totalCount,
                dataDto
            );
        }
    }
}
