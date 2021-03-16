using Grintsys.EasyPOS.CreateUpdateDtos;
using Grintsys.EasyPOS.Dtos;
using Grintsys.EasyPOS.Models;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.AppServices
{
    public class ProductAppService :
        CrudAppService<
            Product,
            ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDto>,
        IProductAppService
    {
        public ProductAppService(IRepository<Product, Guid> repository) : base(repository)
        {

        }
    }
}
