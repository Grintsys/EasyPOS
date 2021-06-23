using System;
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
        public ConfigurationManagerAppService(IRepository<ConfigurationManager, Guid> repository) : base(repository)
        {
        }
    }
}
