using Grintsys.EasyPOS.Product;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.SAP
{
    public class SapManager : ISapManager
    {
        private readonly IConfiguration _settingProvider;
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Customer.Customer> _customerRepository;
        private readonly IRepository<ProductWarehouse> _productWarehouseRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<Sincronizador.Sincronizador> _syncRepository;
        private readonly ILogger<SapManager> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SapManager(IConfiguration settingProvider,
            IProductRepository products,
            IRepository<Customer.Customer> customers,
            ILogger<SapManager> logger,
            IRepository<Sincronizador.Sincronizador> syncRepository,
            IServiceScopeFactory serviceScopeFactory, 
            IRepository<ProductWarehouse> productWarehouseRepository, 
            IRepository<Warehouse> warehouseRepository)
        {
            _settingProvider = settingProvider;
            _productRepository = products;
            _customerRepository = customers;
            _logger = logger;
            _syncRepository = syncRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _productWarehouseRepository = productWarehouseRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<SapResponse> CreateCreditNoteAsync(CreateOrUpdateSalesOrder input)
        {
            Company oCompany = GetCompany();

            var companyResponse = oCompany.Connect();
            var sapMessage = string.Format("Successfully added Sales Order DocEntry: {0}", oCompany.GetNewObjectKey());
            var isSuccess = true;

            var syncRecord = new Sincronizador.Sincronizador()
            {
                TipoTransaccion = Enums.Transacciones.CreacionNotaCredito,
                Estado = Enums.SyncEstados.Transferred,
                Data = JsonConvert.SerializeObject(input)
            };

            _logger.LogInformation("Creating CreditNote");

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);
                _logger.LogError(errorMessage, errorCode);

                syncRecord.Message = errorMessage;
                syncRecord.Estado = Enums.SyncEstados.Failed;

                await _syncRepository.InsertAsync(syncRecord);

                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            IDocuments document = (IDocuments)oCompany.GetBusinessObject(BoObjectTypes.oCreditNotes);
            document.CardCode = input.CustomerCode;
            document.CardName = input.CustomerName;
            document.Comments = $"Creado por {nameof(SapManager)} en EasyPOS";
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
                document.Lines.Add();
            }
            // add Sales Order
            if (document.Add() != 0)
            {
                sapMessage = "Error Code: "
                        + oCompany.GetLastErrorCode().ToString()
                        + " - "
                        + oCompany.GetLastErrorDescription();

                syncRecord.Estado = Enums.SyncEstados.Failed;

                _logger.LogError(sapMessage);
            }

            //recomended from http://www.appseconnect.com/di-api-memory-leak-in-sap-business-one-9-0/
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(salesOrder);
            oCompany.Disconnect();
            _logger.LogInformation("Successfully created");

            var response =
                new SapResponse
                {
                    IsSuccess = isSuccess,
                    Message = sapMessage
                };

            syncRecord.Message = response.Message;
            await _syncRepository.InsertAsync(syncRecord);

            return response;
        }

        public async Task<SapResponse> CreateCustomerAsync(CreateOrUpdateCustomer input)
        {
            Company company = GetCompany();
            var companyResponse = company.Connect();
            var sapMessage = string.Format("Successfully added new bussiness partner DocEntry: {0}", company.GetNewObjectKey());
            var isSuccess = true;

            var syncRecord = new Sincronizador.Sincronizador()
            {
                TipoTransaccion = Enums.Transacciones.CreacionCliente,
                Data = JsonConvert.SerializeObject(input),
                Estado = Enums.SyncEstados.Transferred
            };

            if (companyResponse != 0)
            {
                company.GetLastError(out int errorCode, out string errorMessage);

                syncRecord.Message = errorMessage;
                syncRecord.Estado = Enums.SyncEstados.Failed;

                await _syncRepository.InsertAsync(syncRecord);

                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            try
            {
                _logger.LogInformation("Creating Customer");

                IBusinessPartners document = (IBusinessPartners)company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                document.CardName = input.CustomerName;
                document.CardCode = input.CustomerCode;
                document.Phone1 = input.Phone1;
                document.Phone2 = input.Phone2;
                document.Address = input.Address;
                document.CardType = BoCardTypes.cCustomer;
                document.SalesPersonCode = input.SalesPersonCode;
                //It's a default field
                document.FederalTaxID = "000000000000";

                if (document.UserFields.Fields.Count > 0)
                {
                    document.UserFields.Fields.Item("u_rtn").Value = input.RTN;
                    document.UserFields.Fields.Item("u_cedula").Value = input.Cedula;
                }

                if (document.Add() != 0)
                {
                    isSuccess = false;
                    sapMessage = "Error Code: "
                            + company.GetLastErrorCode().ToString()
                            + " - "
                            + company.GetLastErrorDescription();

                    syncRecord.Estado = Enums.SyncEstados.Failed;

                    _logger.LogError(sapMessage);
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
                sapMessage = e.Message;

                syncRecord.Estado = Enums.SyncEstados.Failed;

                _logger.LogError(sapMessage);
            }

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(businessPartner);
            company.Disconnect();
            _logger.LogInformation("Successfull created");

            syncRecord.Message = sapMessage;
            await _syncRepository.InsertAsync(syncRecord);

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

            var syncRecord = new Sincronizador.Sincronizador()
            {
                TipoTransaccion = Enums.Transacciones.CreacionNotaDebito,
                Data = JsonConvert.SerializeObject(input),
                Estado = Enums.SyncEstados.Transferred
            };

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);

                syncRecord.Message = errorMessage;
                syncRecord.Estado = Enums.SyncEstados.Failed;

                await _syncRepository.InsertAsync(syncRecord);

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

                syncRecord.Estado = Enums.SyncEstados.Failed;

                _logger.LogError(sapMessage);
            }

            //TODO:
            //update order status
            _logger.LogInformation("successfull created");

            //recomended from http://www.appseconnect.com/di-api-memory-leak-in-sap-business-one-9-0/
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(salesOrder);
            oCompany.Disconnect();

            var response =
                new SapResponse
                {
                    IsSuccess = isSuccess,
                    Message = sapMessage
                };

            syncRecord.Message = sapMessage;

            await _syncRepository.InsertAsync(syncRecord);

            return response;
        }

        public async Task<SapResponse> CreateSalesOrderAsync(CreateOrUpdateSalesOrder input)
        {
            _logger.LogInformation("Creating sales order");

            Company oCompany = GetCompany();

            var companyResponse = oCompany.Connect();
            var sapMessage = string.Format("Successfully added Sales Order DocEntry: {0}", oCompany.GetNewObjectKey());
            var isSuccess = true;

            var syncRecord = new Sincronizador.Sincronizador()
            {
                TipoTransaccion = Enums.Transacciones.CreacionOrden,
                Data = JsonConvert.SerializeObject(input),
                Estado = Enums.SyncEstados.Transferred
            };

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);

                syncRecord.Message = errorMessage;
                syncRecord.Estado = Enums.SyncEstados.Failed;

                await _syncRepository.InsertAsync(syncRecord);

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
                document.Lines.Add();
            }
            // add Sales Order
            if (document.Add() != 0)
            {
                sapMessage = "Error Code: "
                        + oCompany.GetLastErrorCode().ToString()
                        + " - "
                        + oCompany.GetLastErrorDescription();

                syncRecord.Estado = Enums.SyncEstados.Failed;

                _logger.LogError(sapMessage);
            }

            _logger.LogInformation("Successfully created");

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

            syncRecord.Message = sapMessage;

            await _syncRepository.InsertAsync(syncRecord);

            return response;
        }

        public async Task<CompanyMetadataDto> GetCompanyMetadata()
        {
            _logger.LogInformation("Getting company metada");

            Company oCompany = GetCompany();
            var companyResponse = oCompany.Connect();

            var syncRecord = new Sincronizador.Sincronizador()
            {
                TipoTransaccion = Enums.Transacciones.SyncMetadata,
                Data = string.Empty,
                Estado = Enums.SyncEstados.Transferred
            };

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);

                syncRecord.Message = errorMessage;
                syncRecord.Estado = Enums.SyncEstados.Failed;

                await _syncRepository.InsertAsync(syncRecord);

                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            Recordset oRecordSet = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            var query = _settingProvider.GetValue<string>("Queries:Banks");

            oRecordSet.DoQuery(query);

            string stringData = oRecordSet.GetAsXML();

            oCompany.Disconnect();

            XmlDocument xml = new();
            xml.LoadXml(stringData);
            var nodeList = xml.SelectNodes("/BOM/BO/DSC1/row");

            var banks = nodeList.Cast<XmlNode>()
                .Select(a => new BankDto
                {
                    BankCode = a.SelectSingleNode("BankCode").InnerText,
                    Account = a.SelectSingleNode("Account").InnerText,
                })
                .ToList();


            //TODO: implement taxes information and get currency
            var companyMetadata = new CompanyMetadataDto
            {
                Currency = "GT",
                Banks = banks
            };

            syncRecord.Message = "Succesfully";
            await _syncRepository.InsertAsync(syncRecord);

            return companyMetadata;
        }

        public async Task<List<CustomerDto>> GetCustomerListAsync(int skipCount = 100)
        {
            Company oCompany = GetCompany();
            var companyResponse = oCompany.Connect();

            var syncRecord = new Sincronizador.Sincronizador()
            {
                TipoTransaccion = Enums.Transacciones.SyncClientes,
                Data = string.Empty,
                Estado = Enums.SyncEstados.Transferred
            };

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);

                syncRecord.Message = errorMessage;
                syncRecord.Estado = Enums.SyncEstados.Failed;

                await _syncRepository.InsertAsync(syncRecord);

                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            Recordset oRecordSet = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            var query = _settingProvider.GetValue<string>("Queries:Customers");

            oRecordSet.DoQuery(query);

            string stringData = oRecordSet.GetAsXML();

            oCompany.Disconnect();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(stringData);
            var nodeList = xml.SelectNodes("/BOM/BO/OCRD/row");

            var customers = nodeList.Cast<XmlNode>()
                .Select(a => new CustomerDto
                {
                    CardCode = a.SelectSingleNode("CardCode").InnerText,
                    CardName = a.SelectSingleNode("CardName").InnerText,
                    Address = a.SelectSingleNode("Address")?.InnerText,
                    Celular = a.SelectSingleNode("Cellular")?.InnerText,
                    Phone1 = a.SelectSingleNode("Phone1")?.InnerText,
                    Phone2 = a.SelectSingleNode("Phone2")?.InnerText,
                    City = a.SelectSingleNode("City")?.InnerText,
                    Balance = decimal.Parse(a.SelectSingleNode("Balance")?.InnerText),
                    RTN = a.SelectSingleNode("U_rtn")?.InnerText,
                    Cedula = a.SelectSingleNode("U_cedula")?.InnerText,
                    CartType = char.Parse(a.SelectSingleNode("CardType").InnerText),
                    CreateDate = DateTime.ParseExact(a.SelectSingleNode("CreateDate").InnerText.Insert(4, "/").Insert(7, "/"), "yyyy/MM/dd", new System.Globalization.CultureInfo("en-US")),
                    Email = a.SelectSingleNode("E_mail")?.InnerText
                })
                .ToList();

            syncRecord.Message = "Successfully";

            await _syncRepository.InsertAsync(syncRecord);

            return customers.Take(skipCount).ToList();
        }

        public async Task<List<ProductDto>> GetProductListAsync(int skipCount = 100)
        {
            Company oCompany = GetCompany();
            var companyResponse = oCompany.Connect();

            var syncRecord = new Sincronizador.Sincronizador()
            {
                TipoTransaccion = Enums.Transacciones.SyncProductos,
                Data = string.Empty,
                Estado = Enums.SyncEstados.Transferred
            };

            if (companyResponse != 0)
            {
                oCompany.GetLastError(out int errorCode, out string errorMessage);

                syncRecord.Message = errorMessage;
                syncRecord.Estado = Enums.SyncEstados.Failed;

                await _syncRepository.InsertAsync(syncRecord);

                throw new ArgumentException($"{errorCode}: {errorMessage}");
            }

            Recordset oRecordSet = oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            var query = _settingProvider.GetValue<string>("Queries:Items");

            oRecordSet.DoQuery(query);

            string stringData = oRecordSet.GetAsXML();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(stringData);
            var nodeList = xml.SelectNodes("/BOM/BO/OITW/row");

            var products = nodeList.Cast<XmlNode>()
                .Select(a => new ProductDto
                {
                    ItemCode = a.SelectSingleNode("ItemCode").InnerText,
                    ItemName = a.SelectSingleNode("ItemName").InnerText,
                    OnHand = double.Parse(a.SelectSingleNode("OnHand").InnerText),
                    SalesPrice = double.Parse(a.SelectSingleNode("AvgPrice").InnerText),
                    WhsCode = a.SelectSingleNode("WhsCode").InnerText
                })
                .ToList();

            syncRecord.Message = "Successfully";

            await _syncRepository.InsertAsync(syncRecord);

            return products.Take(skipCount).ToList();
        }

        public async Task UpsertProducts()
        {
            var savedDict = new Dictionary<string, Guid>();
            var items = await GetProductListAsync();

            foreach (var item in items)
            {
                var productToUpdate = await _productRepository.FirstOrDefaultAsync(x => x.Code == item.ItemCode);

                var hasProduct = !(productToUpdate is null);

                if (hasProduct && item.OnHand > 0)
                {
                    await UpsertProductWarehouse(savedDict, item.WhsCode, productToUpdate.Id, (int)item.OnHand);
                }
                else if(!hasProduct && item.OnHand > 0)
                {
                    var prodId = Guid.Empty;
                    
                    var dictResult = savedDict.FirstOrDefault(x => x.Key == item.ItemCode);
                    if (dictResult.Key != null)
                    {
                        prodId = dictResult.Value;
                    }
                    else
                    {
                        var productDto = MapProduct(item);
                        var product = await _productRepository.InsertAsync(productDto);
                        savedDict.Add(product.Code, product.Id);
                        prodId = product.Id;
                    }
                    
                    await UpsertProductWarehouse(savedDict, item.WhsCode, prodId, (int)item.OnHand);
                }
            }
        }

        public async Task UpsertCustomers()
        {
            var items = await GetCustomerListAsync();

            foreach (var item in items)
            {
                var customertToUpdate = _customerRepository.FirstOrDefault(x => x.Code == item.CardCode);
                var customer = MapCustomer(item);
                var hasCustomer = !(customertToUpdate is null);

                if (hasCustomer)
                {
                    await _customerRepository.UpdateAsync(customertToUpdate);
                }
                else
                {
                    await _customerRepository.InsertAsync(customer);
                }
            }
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

        private Customer.Customer MapCustomer(CustomerDto customer)
        {
            return new Customer.Customer
            {
                Code = customer.CardCode,
                FirstName = customer.CardName,
                Address = customer.Address,
                RTN = customer.RTN,
                PhoneNumber = customer.Phone1,
                IdNumber = customer.Cedula,
                Status = Enums.CustomerStatus.Transferred
            };
        }

        private Product.Product MapProduct(ProductDto item)
        {
            var product = new Product.Product()
            {
                Code = item.ItemCode,
                Name = item.ItemName,
                Description = item.ItemName,
                SalePrice = (float)item.SalesPrice,
                Taxes = item.HasTaxes
            };

            return product;
        }

        private async Task UpsertProductWarehouse(Dictionary<string, Guid> saved, string warehouseCode, Guid productId, int stock)
        {

            var warehouse = await _warehouseRepository.FirstOrDefaultAsync(x => x.Code == warehouseCode);
            var wareHouseId = warehouse != null ? warehouse.Id : Guid.Empty;

            if(warehouse == null)
            {
                var dicResult = saved.FirstOrDefault(x => x.Key == warehouseCode);

                if(dicResult.Key == null)
                {
                    warehouse = await _warehouseRepository.InsertAsync(new Warehouse()
                    {
                        Code = warehouseCode,
                        Name = "Bodega " + warehouseCode
                    });

                    wareHouseId = warehouse.Id;

                    saved.Add(warehouse.Code, warehouse.Id);
                }
                else
                {
                    wareHouseId = dicResult.Value;
                }
            }

            var productWarehouseDto = new Product.ProductWarehouse()
            {
                WarehouseId = wareHouseId,
                Inventory = stock,
                ProductId = productId,
            };

            await _productWarehouseRepository.InsertAsync(productWarehouseDto);
        }
    }

}
