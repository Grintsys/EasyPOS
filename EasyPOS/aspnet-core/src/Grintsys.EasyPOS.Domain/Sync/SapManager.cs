using Newtonsoft.Json;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Settings;

namespace Grintsys.EasyPOS.Sync
{
    public class SapManager : ISapManager
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IRepository<Product.Product> _productRepository;

        public SapManager(ISettingProvider settingProvider, IRepository<Product.Product> products)
        {
            _settingProvider = settingProvider;
            _productRepository = products;
        }

        private async Task<Company> GetCompany() {
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

        public async Task<SapResponse> CreateCustomerAsync(CreateOrUpdateCustomer input)
        {
            Company company = await GetCompany();
            var companyResponse = company.Connect();
            var sapMessage = string.Format("Successfully added new bussiness partner DocEntry: {0}", company.GetNewObjectKey());
            var isSuccess = true;

            if (companyResponse != 0)
            {
                company.GetLastError(out int errorCode, out string errorMessage);
                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            IDocuments businessPartner = (IDocuments)company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            businessPartner.CardName = input.CustomerName;
            businessPartner.CardCode = input.CustomerCode;
            businessPartner.Address = input.Address;
            businessPartner.Comments = $"Creado por {nameof(SapManager)} en EasyPOS";
            businessPartner.SalesPersonCode = input.SAlesPersonCode;

            if (businessPartner.UserFields.Fields.Count > 0)
            {
                businessPartner.UserFields.Fields.Item("U_RTN").Value = input.RTN;
            }                    

            if (businessPartner.Add() != 0)
            {
                isSuccess = false;
                sapMessage = "Error Code: "
                        + company.GetLastErrorCode().ToString()
                        + " - "
                        + company.GetLastErrorDescription();
            }

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(businessPartner);
            company.Disconnect();

            return new SapResponse
            {
                IsSuccess = isSuccess,
                Message = sapMessage
            };
        }

        public async Task<SapResponse> CreateInvoiceAsync(CreateOrUpdateInvoiceDto input)
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

            IDocuments salesOrder = (IDocuments)company.GetBusinessObject(BoObjectTypes.oOrders);
            salesOrder.CardCode = input.CustomerCode;
            salesOrder.CardName = input.CustomerName;
            salesOrder.Comments = $"Creado por {nameof(SapManager)} en EasyPOS";
            //salesOrder.Series = input.Series;
            salesOrder.SalesPersonCode = input.SalesPersonId;
            salesOrder.DocDueDate = input.CreatedDate;

            //if (salesOrder.UserFields.Fields.Count > 0)
            //{
            //    salesOrder.UserFields.Fields.Item("U_FacNit").Value = client == null ? string.Empty : string.IsNullOrWhiteSpace(client.RTN) ? string.Empty : client.RTN;
            //    salesOrder.UserFields.Fields.Item("U_M2_UUID").Value = order.U_M2_UUID.ToString();
            //}

            foreach (var item in input.Lines)
            {
                salesOrder.Lines.ItemCode = item.Code;
                salesOrder.Lines.Quantity = item.Quantity;
                //salesOrder.Lines.TaxCode = item.Taxes;
                salesOrder.Lines.DiscountPercent = item.Discount;
                salesOrder.Lines.WarehouseCode = input.WarehouseCode;
                ////settigs by tenant
                //salesOrder.Lines.CostingCode = tenant.CostingCode;
                //salesOrder.Lines.CostingCode2 = tenant.CostingCode2;
                //salesOrder.Lines.CostingCode3 = dimension;
                salesOrder.Lines.Add();
            }
            // add Sales Order
            if (salesOrder.Add() != 0)
            {
                sapMessage = "Error Code: "
                        + company.GetLastErrorCode().ToString()
                        + " - "
                        + company.GetLastErrorDescription();
            }

            //TODO:
            //update order status

            //recomended from http://www.appseconnect.com/di-api-memory-leak-in-sap-business-one-9-0/
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(salesOrder);
            company.Disconnect();

            return new SapResponse
            {
                IsSuccess = isSuccess,
                Message = sapMessage
            };
        }

        public async Task<List<CustomerDto>> GetCustomerListAsync()
        {
            using StreamReader r = new(@"Data\customersDummy.json");
            string json = await r.ReadToEndAsync();
            List<CustomerDto> items = JsonConvert.DeserializeObject<List<CustomerDto>>(json);
            return items;
        }

        public async Task<List<ProductDto>> GetProductListAsync()
        {
            using StreamReader r = new(@"Data\productsDummy.json");
            string json = await r.ReadToEndAsync();
            List<ProductDto> items = JsonConvert.DeserializeObject<List<ProductDto>>(json);

            foreach (var item in items)
            {
                var productToUpdate = _productRepository.FirstOrDefault(x => x.Code == item.ItemCode);
                var product = MapProduct(item);
                var hasProduct = !(productToUpdate is null);

                if (hasProduct)
                {
                    await _productRepository.UpdateAsync(product);
                }
                else
                {
                    await _productRepository.InsertAsync(product);
                }

            }
            return items;
        }

        private Product.Product MapProduct(ProductDto item) =>
           new Product.Product
           {
               Name = item.ItemName,
               Code = item.ItemCode,
               //ProductWarehouse = item.WarehouseCode,
               Description = item.ItemName,
               SalePrice = item.SalesPrice,
           };
    }
}
