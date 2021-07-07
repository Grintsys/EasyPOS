using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.TenantManagement;

namespace Grintsys.EasyPOS.ConfigurationManager
{
    //[Authorize("Conf_Management")]
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
        private readonly IRepository<Tenant, Guid> _tenantRepository;


        public ConfigurationManagerAppService(
            IRepository<ConfigurationManager, Guid> repository, 
            IRepository<Tenant, Guid> tenantRepository) : base(repository)
        {
            _configRepository = repository;
            _tenantRepository = tenantRepository;
        }

        //[Authorize("Listar_Conf")]
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
                   ).ToList();
            }

            return dto.OrderBy(x => x.Key).ToList();
        }

        [AllowAnonymous]
        public async Task<List<object>> ReturnAllTenants()
        {
            var data = await _tenantRepository.GetListAsync();

            var dto = new List<object>(ObjectMapper.Map<List<Tenant>, List<object>>(data));
            return dto;
        }
    }
}
