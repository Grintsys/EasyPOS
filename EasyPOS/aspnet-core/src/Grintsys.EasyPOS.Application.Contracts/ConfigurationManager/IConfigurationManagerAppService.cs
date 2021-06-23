using System;
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
    }
}
