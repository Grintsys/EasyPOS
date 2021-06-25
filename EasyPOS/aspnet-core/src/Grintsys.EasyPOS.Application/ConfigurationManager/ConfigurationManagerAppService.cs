using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.ConfigurationManager
{
    public class ConfigurationManagerAppService :
        CrudAppService<
            ConfigurationManager,
            ConfigurationManagerDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateConfigurationManagerDto>,
        IConfigurationManagerAppService
    {
        private readonly IRepository<ConfigurationManager, Guid> _configRepository;

        public ConfigurationManagerAppService(IRepository<ConfigurationManager, Guid> repository, IRepository<ConfigurationManager, Guid> configRepository) : base(repository)
        {
            _configRepository = configRepository;
        }
        public async Task<List<ConfigurationManagerDto>> GetConfigList(string filter)
        {
            var data = await _configRepository.GetListAsync();
            var dto = new List<ConfigurationManagerDto>(ObjectMapper.Map<List<ConfigurationManager>, List<ConfigurationManagerDto>>(data));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.Key.ToLower().Contains(filter)
                    || x.Value.ToLower().Contains(filter)
                   ).OrderBy(x => x.Key).ToList();
            }

            return dto;
        }
    }
}
