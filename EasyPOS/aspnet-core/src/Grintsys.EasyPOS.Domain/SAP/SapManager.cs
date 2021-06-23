using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.SAP
{
    public class SapManager : ISapManager
    {
        private readonly IConfiguration _settingProvider;
        private readonly IRepository<Product.Product> _productRepository;

        public SapManager(IConfiguration settingProvider, 
            IRepository<Product.Product> products)
        {
            _settingProvider = settingProvider;
            _productRepository = products;
        }

        public async Task<SapResponse> CreateCreditNoteAsync(CreateOrUpdateSalesOrder input)
        {
            Company oCompany = GetCompany();

            var companyResponse = oCompany.Connect();
            var sapMessage = string.Format("Successfully added Sales Order DocEntry: {0}", oCompany.GetNewObjectKey());
            var isSuccess = true;

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);
                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            IDocuments document = (IDocuments)oCompany.GetBusinessObject(BoObjectTypes.oCreditNotes);
            document.CardCode = input.CustomerCode;
            document.CardName = input.CustomerName;
            document.Comments = $"Creado por {nameof(SapManager)} en EasyPOS";
            //salesOrder.Series = input.Series;
            document.SalesPersonCode = input.SalesPersonId;
            document.DocDueDate = input.CreatedDate;

            //if (salesOrder.UserFields.Fields.Count > 0)
            //{
            //    salesOrder.UserFields.Fields.Item("U_FacNit").Value = client == null ? string.Empty : string.IsNullOrWhiteSpace(client.RTN) ? string.Empty : client.RTN;
            //    salesOrder.UserFields.Fields.Item("U_M2_UUID").Value = order.U_M2_UUID.ToString();
            //}

            foreach (var item in input.Lines)
            {
                document.Lines.ItemCode = item.Code;
                document.Lines.Quantity = item.Quantity;
                //salesOrder.Lines.TaxCode = item.Taxes;
                document.Lines.DiscountPercent = item.Discount;
                document.Lines.WarehouseCode = input.WarehouseCode;
                ////settigs by tenant
                //salesOrder.Lines.CostingCode = tenant.CostingCode;
                //salesOrder.Lines.CostingCode2 = tenant.CostingCode2;
                //salesOrder.Lines.CostingCode3 = dimension;
                document.Lines.Add();
            }
            // add Sales Order
            if (document.Add() != 0)
            {
                sapMessage = "Error Code: "
                        + oCompany.GetLastErrorCode().ToString()
                        + " - "
                        + oCompany.GetLastErrorDescription();
            }

            //TODO:
            //update order status

            //recomended from http://www.appseconnect.com/di-api-memory-leak-in-sap-business-one-9-0/
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(salesOrder);
            oCompany.Disconnect();

            var response =
                new SapResponse
                {
                    IsSuccess = isSuccess,
                    Message = sapMessage
                };

            return response;
        }

        public async Task<SapResponse> CreateCustomerAsync(CreateOrUpdateCustomer input)
        {
            Company company = GetCompany();
            var companyResponse = company.Connect();
            var sapMessage = string.Format("Successfully added new bussiness partner DocEntry: {0}", company.GetNewObjectKey());
            var isSuccess = true;

            if (companyResponse != 0)
            {
                company.GetLastError(out int errorCode, out string errorMessage);
                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            try
            {
                IBusinessPartners document = (IBusinessPartners)company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                document.CardName = input.CustomerName;
                document.CardCode = input.CustomerCode;
                document.Address = input.Address;
                document.CardType = BoCardTypes.cCustomer;
                document.SalesPersonCode = input.SalesPersonCode;
                document.FederalTaxID = "000000000000";

                if (document.UserFields.Fields.Count > 0)
                {
                    //document.UserFields.Fields.Item("U_NIT").Value = input.RTN;
                }

                if (document.Add() != 0)
                {
                    isSuccess = false;
                    sapMessage = "Error Code: "
                            + company.GetLastErrorCode().ToString()
                            + " - "
                            + company.GetLastErrorDescription();
                }
            }
            catch(Exception e)
            {
                isSuccess = false;
                sapMessage = e.Message;
            }          

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(businessPartner);
            company.Disconnect();

            var response = new SapResponse
            {
                IsSuccess = isSuccess,
                Message = sapMessage
            };

            return response;
        }

        public async Task<SapResponse> CreateDebitNoteAsync(CreateOrUpdateSalesOrder input)
        {
            Company oCompany = GetCompany();

            var companyResponse = oCompany.Connect();
            var sapMessage = string.Format("Successfully added Sales Order DocEntry: {0}", oCompany.GetNewObjectKey());
            var isSuccess = true;

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);
                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            IDocuments document = (IDocuments)oCompany.GetBusinessObject(BoObjectTypes.oCorrectionInvoice);
            document.CardCode = input.CustomerCode;
            document.CardName = input.CustomerName;
            document.Comments = $"Creado por {nameof(SapManager)} en EasyPOS";
            //salesOrder.Series = input.Series;
            document.SalesPersonCode = input.SalesPersonId;
            document.DocDueDate = input.CreatedDate;

            //if (salesOrder.UserFields.Fields.Count > 0)
            //{
            //    salesOrder.UserFields.Fields.Item("U_FacNit").Value = client == null ? string.Empty : string.IsNullOrWhiteSpace(client.RTN) ? string.Empty : client.RTN;
            //    salesOrder.UserFields.Fields.Item("U_M2_UUID").Value = order.U_M2_UUID.ToString();
            //}

            foreach (var item in input.Lines)
            {
                document.Lines.ItemCode = item.Code;
                document.Lines.Quantity = item.Quantity;
                //salesOrder.Lines.TaxCode = item.Taxes;
                document.Lines.DiscountPercent = item.Discount;
                document.Lines.WarehouseCode = input.WarehouseCode;
                ////settigs by tenant
                //salesOrder.Lines.CostingCode = tenant.CostingCode;
                //salesOrder.Lines.CostingCode2 = tenant.CostingCode2;
                //salesOrder.Lines.CostingCode3 = dimension;
                document.Lines.Add();
            }
            // add Sales Order
            if (document.Add() != 0)
            {
                sapMessage = "Error Code: "
                        + oCompany.GetLastErrorCode().ToString()
                        + " - "
                        + oCompany.GetLastErrorDescription();
            }

            //TODO:
            //update order status

            //recomended from http://www.appseconnect.com/di-api-memory-leak-in-sap-business-one-9-0/
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(salesOrder);
            oCompany.Disconnect();

            var response =
                new SapResponse
                {
                    IsSuccess = isSuccess,
                    Message = sapMessage
                };

            return response;
        }

        public async Task<SapResponse> CreateSalesOrderAsync(CreateOrUpdateSalesOrder input)
        {
            Company oCompany = GetCompany();

            var companyResponse = oCompany.Connect();
            var sapMessage = string.Format("Successfully added Sales Order DocEntry: {0}", oCompany.GetNewObjectKey());
            var isSuccess = true;

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);
                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            IDocuments document = (IDocuments)oCompany.GetBusinessObject(BoObjectTypes.oOrders);
            document.CardCode = input.CustomerCode;
            document.CardName = input.CustomerName;
            document.Comments = $"Creado por {nameof(SapManager)} en EasyPOS";
            //salesOrder.Series = input.Series;
            document.SalesPersonCode = input.SalesPersonId;
            document.DocDueDate = input.CreatedDate;

            //if (salesOrder.UserFields.Fields.Count > 0)
            //{
            //    salesOrder.UserFields.Fields.Item("U_FacNit").Value = client == null ? string.Empty : string.IsNullOrWhiteSpace(client.RTN) ? string.Empty : client.RTN;
            //    salesOrder.UserFields.Fields.Item("U_M2_UUID").Value = order.U_M2_UUID.ToString();
            //}

            foreach (var item in input.Lines)
            {
                document.Lines.ItemCode = item.Code;
                document.Lines.Quantity = item.Quantity;
                //salesOrder.Lines.TaxCode = item.Taxes;
                document.Lines.DiscountPercent = item.Discount;
                document.Lines.WarehouseCode = input.WarehouseCode;
                ////settigs by tenant
                //salesOrder.Lines.CostingCode = tenant.CostingCode;
                //salesOrder.Lines.CostingCode2 = tenant.CostingCode2;
                //salesOrder.Lines.CostingCode3 = dimension;
                document.Lines.Add();
            }
            // add Sales Order
            if (document.Add() != 0)
            {
                sapMessage = "Error Code: "
                        + oCompany.GetLastErrorCode().ToString()
                        + " - "
                        + oCompany.GetLastErrorDescription();
            }

            //TODO:
            //update order status

            //recomended from http://www.appseconnect.com/di-api-memory-leak-in-sap-business-one-9-0/
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(salesOrder);
            oCompany.Disconnect();

            var response =
                new SapResponse
                {
                    IsSuccess = isSuccess,
                    Message = sapMessage
                };

            return response;
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

        private Company GetCompany()
        {
            var oCompany = new Company();

            oCompany.Server = _settingProvider.GetValue<string>("SAP:Server");
            oCompany.SLDServer = _settingProvider.GetValue<string>("SAP:SLDServer");
            oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016;
            oCompany.CompanyDB = _settingProvider.GetValue<string>("SAP:CompanyDB");
            oCompany.UserName = _settingProvider.GetValue<string>("SAP:UserName");
            oCompany.Password = _settingProvider.GetValue<string>("SAP:Password");
            oCompany.DbUserName = _settingProvider.GetValue<string>("SAP:DbUserName");
            oCompany.DbPassword = _settingProvider.GetValue<string>("SAP:DbPassword");

            return oCompany;
        }

        private Product.Product MapProduct(ProductDto item) =>
           new()
           {
               Name = item.ItemName,
               Code = item.ItemCode,
               //ProductWarehouse = item.WarehouseCode,
               Description = item.ItemName,
               SalePrice = item.SalesPrice,
           };
    }

}
