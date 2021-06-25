using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.ConfigurationManager
{
    public interface IConfigurationManagerAppService :
        ICrudAppService<
            ConfigurationManagerDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateConfigurationManagerDto
        >
    {
        Task<List<ConfigurationManagerDto>> GetConfigList(string filter);
    }
}
