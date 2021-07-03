using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grintsys.EasyPOS.Enums;
using Grintsys.EasyPOS.PaymentMethod;
using Grintsys.EasyPOS.Product;
using Newtonsoft.Json;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Grintsys.EasyPOS.Seed
{
    public class EasyPOSDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Customer.Customer, Guid> _customerRepository;
        private readonly IRepository<Product.Product, Guid> _productRepository;
        private readonly IRepository<Warehouse, Guid> _warehouseRepository;
        private readonly IRepository<ProductWarehouse, Guid> _productWarehousesRepository;
        private readonly IRepository<ConfigurationManager.ConfigurationManager, Guid> _configurationManagerRepository;
        private readonly IRepository<Sincronizador.Sincronizador, Guid> _syncRepository;

        public EasyPOSDataSeederContributor(
            IRepository<Customer.Customer, Guid> customerRepository,
            IRepository<Product.Product, Guid> productRepository,
            IRepository<PaymentMethod.PaymentMethod, Guid> paymentMethodRepository,
            IRepository<Warehouse, Guid> warehouseRepository,
            IRepository<ProductWarehouse, Guid> productWarehousesRepository,
            IRepository<Sincronizador.Sincronizador, Guid> syncRepository, 
            IRepository<ConfigurationManager.ConfigurationManager, Guid> configurationManagerRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _productWarehousesRepository = productWarehousesRepository;
            _syncRepository = syncRepository;
            _configurationManagerRepository = configurationManagerRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var prod1 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Coca Cola 12oz",
                    Description = "The Coca-Cola - Classic 12oz cans",
                    Code = "Code1",
                    SalePrice = 1.36f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "http:cdn.shopify.com/s/files/1/0358/4714/3560/products/COKECAN_1200x1200.jpg?v=1584912010"
                });

            var prod2 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Doritos Flamin' Hot 9.75oz",
                    Description = "Doritos Tortilla Chips, Flamin' Hot flavor, 9.75 Ounce (Pack of 1)",
                    Code = "Code2",
                    SalePrice = 8.86f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:images-na.ssl-images-amazon.com/images/I/81H-aEOo09L._SL1500_.jpg"
                });

            var prod3 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Cheetos Flamin' Hot 1oz",
                    Description = "Cheetos Crunchy Flamin' Hot Cheese Flavored Snacks, 1 Ounce (Pack of 40)",
                    Code = "Code3",
                    SalePrice = 16.98f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:http2.mlstatic.com/D_NQ_NP_681966-MCO32215719280_092019-O.jpg"
                });

            var prod4 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Coca Cola 12oz",
                    Description = "The Coca-Cola - Classic 12oz cans (Pack of 18)",
                    Code = "Code4",
                    SalePrice = 20.50f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:images-na.ssl-images-amazon.com/images/I/81GsS1XFEZL._AC_SX569_.jpg"
                });

            var prod5 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Sprite, Lemon-Lime Soda 12oz",
                    Description = "Lemon-Lime Soda, 100% Natural Flavors 12oz cans (Pack of 18)",
                    Code = "Code5",
                    SalePrice = 19.01f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:m.media-amazon.com/images/I/519okNpItHL.jpg"
                });

            var prod6 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Sprite, Lemon-Lime Soda 12oz",
                    Description = "Lemon-Lime Soda, 100% Natural Flavors 12oz cans",
                    Code = "Code6",
                    SalePrice = 1.35f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:5.imimg.com/data5/GT/UV/HS/SELLER-65134076/355ml-sprite-can-500x500.jpg"
                });

            var prod7 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "3 Musketeers Chocolate Candy Bar",
                    Description = "Made with a fluffy, whipped chocolate center and covered in milk chocolate (Pack 24)",
                    Code = "Code7",
                    SalePrice = 35.52f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:i5.walmartimages.com/asr/e8d9a5d0-411a-4658-a396-d9a9ac59e9f5.7e3ba4a3152ab82b2337ad232656f358.jpeg"
                });

            var prod8 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Kit Kat Dark Chocolate Wafer Candy",
                    Description = "Fill snack drawers, lunch boxes and candy displays all year long with dark chocolate and wafer KIT KAT candy bars (Pack 24)",
                    Code = "Code8",
                    SalePrice = 20.40f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:www.hersheys.com/content/dam/smartlabelproductsimage/kitkat/00034000000463-0013.png"
                });

            var prod9 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "M&M'S Peanut Chocolate Candy Singles ,1.74 Ounce (Pack of 48)",
                    Description = "Made with roasted peanuts and real milk chocolate surrounded by a colorful candy shell (Pack of 48)",
                    Code = "Code9",
                    SalePrice = 34.99f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:cdn11.bigcommerce.com/s-2fq65jrvsu/images/stencil/1280x1280/products/817/1986/c-m_m-pea__82865.1592026178.jpg?c=1"
                });

            var prod10 = await _productRepository.InsertAsync(
                new Product.Product()
                {
                    Name = "Milky Way Candy Bars",
                    Description = "MILKY WAY Candy Bars feature creamy caramel and smooth nougat enrobed in rich milk chocolate (Pack 36)",
                    Code = "Code10",
                    SalePrice = 23.35f,
                    Taxes = true,
                    IsActive = true,
                    ImageUrl = "https:resources.sears.com.mx/medios-plazavip/fotos/productos_sears1/original/3125678.jpg"
                });

            var cus1 = await _customerRepository.InsertAsync(
                new Customer.Customer()
                {
                    FullName = "Jane Doe",
                    IdNumber = "1618199791312",
                    RTN = "16181997913120",
                    Address = "San Pedro Sula, Colonia Los Alamos",
                    PhoneNumber = "(504)2659-8380",
                    Status = CustomerStatus.Created,
                    Code = "Customer1"
                });

            var cus2 = await _customerRepository.InsertAsync(
                new Customer.Customer()
                {
                    FullName = "John Doe",
                    IdNumber = "1618199791332",
                    RTN = "16181997913130",
                    Address = "San Pedro Sula, Colonia Los Alamos",
                    PhoneNumber = "(504)2659-8360",
                    Status = CustomerStatus.Created,
                    Code = "Customer2"
                });

            var ware1 = await _warehouseRepository.InsertAsync(
                new Warehouse()
                {
                    Address = "San Pedro Sula",
                    Name = "Bodega 1"
                });

            var ware2 = await _warehouseRepository.InsertAsync(
                new Warehouse()
                {
                    Address = "La Ceiba",
                    Name = "Bodega 2"
                });

            var ware3 = await _warehouseRepository.InsertAsync(
                new Warehouse()
                {
                    Address = "Tegucigalpa",
                    Name = "Bodega 3"
                });

            await _productWarehousesRepository.InsertAsync(
               new ProductWarehouse()
               {
                   ProductId = prod1.Id,
                   WarehouseId = ware1.Id,
                   Inventory = 50
               });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod1.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod1.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod2.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod2.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod2.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod3.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod3.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod3.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod10.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod10.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod10.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod4.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod4.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod4.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod5.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod5.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod5.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod6.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod6.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod6.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod7.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod7.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod7.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod8.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod8.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod8.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod9.Id,
                    WarehouseId = ware1.Id,
                    Inventory = 50
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod9.Id,
                    WarehouseId = ware2.Id,
                    Inventory = 75
                });

            await _productWarehousesRepository.InsertAsync(
                new ProductWarehouse()
                {
                    ProductId = prod9.Id,
                    WarehouseId = ware3.Id,
                    Inventory = 100
                });

            var banks = new List<SAP.BankDto>()
            {
                new SAP.BankDto()
                {
                    BankCode = "BP",
                    Account = "Banpais"
                },
                new SAP.BankDto()
                {
                    BankCode = "BAC",
                    Account = "Bac Credomatic"
                },
                new SAP.BankDto()
                {
                    BankCode = "FIC",
                    Account = "Ficohsa"
                },
                new SAP.BankDto()
                {
                    BankCode = "BPr",
                    Account = "Banco Promerica"
                },
            };

            await _configurationManagerRepository.InsertAsync(
                new ConfigurationManager.ConfigurationManager()
                {
                    Key = "Bancos",
                    Value = JsonConvert.SerializeObject(banks)
                });

            var taxes = new List<object>()
            {
                new
                {
                    TaxCode = "EXE",
                    Tax = "Excento"
                },
                 new
                {
                    TaxCode = "IVA",
                    Tax = "ISV"
                }
            };

            await _configurationManagerRepository.InsertAsync(
                new ConfigurationManager.ConfigurationManager()
                {
                    Key = "Impuestos",
                    Value = JsonConvert.SerializeObject(taxes)
                });

            var currency =
                new
                {
                    Currency = "HNL"
                };


            await _configurationManagerRepository.InsertAsync(
                new ConfigurationManager.ConfigurationManager()
                {
                    Key = "Moneda",
                    Value = JsonConvert.SerializeObject(currency)
                });

            await _syncRepository.InsertAsync(
                new Sincronizador.Sincronizador()
                {
                    TipoTransaccion = Transacciones.CreacionOrden,
                    Estado = SyncEstados.Created,
                    Data = JsonConvert.SerializeObject(banks),
                    Message = string.Empty
                });
        }
    }
}
