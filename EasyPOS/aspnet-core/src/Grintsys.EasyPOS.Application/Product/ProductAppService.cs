using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Settings;

namespace Grintsys.EasyPOS.Product
{
    public class ProductAppService :
        CrudAppService<
            Product,
            ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDto
        >,
        IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISettingProvider _settingProvider;
        public ProductAppService(IRepository<Product, Guid> repository, 
            IProductRepository productRepository, ISettingProvider settingProvider) : base(repository)
        {
            _productRepository = productRepository;
            _settingProvider = settingProvider;
    }

        public async Task<ProductDto> GetProduct(Guid id)
        {
            var data = await _productRepository.GetAsync(id);
            var dto = ObjectMapper.Map<Product, ProductDto>(data);

            //if (warehouseId.HasValue && dto != null)
            //{
            //    dto.ProductWarehouse = dto.ProductWarehouse.Where(x => x.WarehouseId == warehouseId).ToList();
            //}


            try
            {
                Company company = await GetCompany();
                var companyResponse = company.Connect();
                var sapMessage = string.Format("Successfully added Sales Order DocEntry: {0}", company.GetNewObjectKey());
                var isSuccess = true;

                if (companyResponse != 0)
                {
                    company.GetLastError(out int errorCode, out string errorMessage);
                    throw new ArgumentException($"{errorCode}: {errorMessage}");
                }

            }catch(Exception e)
            {
                throw e;
            }

            return dto;
        }



        private async Task<Company> GetCompany()
        {
            return new Company()
            {
                Server = await _settingProvider.GetOrNullAsync("SAP.Server"),
                CompanyDB = await _settingProvider.GetOrNullAsync("SAP.CompanyDB"),
                DbServerType = BoDataServerTypes.dst_MSSQL2014,
                DbUserName = await _settingProvider.GetOrNullAsync("SAP.DbUserName"),
                DbPassword = await _settingProvider.GetOrNullAsync("SAP.DbPassword"),
                UserName = await _settingProvider.GetOrNullAsync("SAP.UserName"),
                Password = await _settingProvider.GetOrNullAsync("SAP.Password"),
                language = BoSuppLangs.ln_Spanish,
                UseTrusted = false,
                LicenseServer = await _settingProvider.GetOrNullAsync("SAP.LicenseServer")
            };
        }

        public async Task<List<ProductDto>> GetProductList(string filter)
        {
            var products = await _productRepository.GetListAsync();
            var dto = new List<ProductDto>(ObjectMapper.Map<List<Product>, List<ProductDto>>(products));

            if (!filter.IsNullOrWhiteSpace())
            {
                filter = filter.ToLower();
                dto = dto.WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(filter)
                            || x.Description.ToLower().Contains(filter)
                            || x.Code.ToLower().Contains(filter))
                         .OrderBy(x => x.Name).ToList();
            }

            //if(warehouseId.HasValue && dto.Any())
            //{
            //    dto = dto.Where(x => x.ProductWarehouse.Select(x => x.WarehouseId == warehouseId).Any()).ToList();
            //}

            return dto;
        }

        public async Task<List<ProductLookupDto>> GetProductLookupAsync()
        {
            var products = await _productRepository.GetListAsync();

            return new List<ProductLookupDto>(
                ObjectMapper.Map<List<Product>, List<ProductLookupDto>>(products)
            );
        }

        public async Task<List<ProductDto>> GetProductListByWarehouseAsync(Guid wareHouseId)
        {
            var data = await _productRepository.GetListAsync();

            var dataDto = await MapToGetListOutputDtosAsync(data);
            dataDto = dataDto.Where(x => x.ProductWarehouse.Select(x => x.WarehouseId == wareHouseId).Any()).ToList();

            return dataDto;
        }

    }
}
