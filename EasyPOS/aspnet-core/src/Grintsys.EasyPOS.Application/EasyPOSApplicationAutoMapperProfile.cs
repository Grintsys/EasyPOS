﻿using AutoMapper;
using Grintsys.EasyPOS.ConfigurationManager;
using Grintsys.EasyPOS.CreditNote;
using Grintsys.EasyPOS.Customer;
using Grintsys.EasyPOS.DebitNote;
using Grintsys.EasyPOS.Order;
using Grintsys.EasyPOS.PaymentMethod;
using Grintsys.EasyPOS.Product;
using Grintsys.EasyPOS.Sincronizador;

namespace Grintsys.EasyPOS
{
    public class EasyPOSApplicationAutoMapperProfile : Profile
    {
        public EasyPOSApplicationAutoMapperProfile()
        {
            CreateMap<Product.Product, ProductDto>();
            CreateMap<Product.Product, ProductLookupDto>();
            CreateMap<CreateUpdateProductDto, Product.Product>();
           
            CreateMap<Customer.Customer, CustomerDto>();
            CreateMap<Customer.Customer, CustomerLookupDto>();
            CreateMap<CreateUpdateCustomerDto, Customer.Customer>();
            
            CreateMap<Order.Order, OrderDto>();
            CreateMap<CreateUpdateOrderDto, Order.Order>();
            
            CreateMap<Order.OrderItem, OrderItemDto>();
            CreateMap<CreateUpdateOrderItemDto, Order.OrderItem>();

            CreateMap<Order.OrderItem, CreateUpdateCreditNoteItemDto>();
            CreateMap<Order.OrderItem, CreateUpdateDebitNoteItemDto>();
            
            CreateMap<CreditNote.CreditNote, CreditNoteDto>();
            CreateMap<CreateUpdateCreditNoteDto, CreditNote.CreditNote>();
            
            CreateMap<CreditNoteItem, CreditNoteItemDto>();
            CreateMap<CreateUpdateCreditNoteItemDto, CreditNoteItem>();

            CreateMap<DebitNote.DebitNote, DebitNoteDto>();
            CreateMap<CreateUpdateDebitNoteDto, DebitNote.DebitNote>();
            
            CreateMap<DebitNoteItem, DebitNoteItemDto>();
            CreateMap<CreateUpdateDebitNoteItemDto, DebitNoteItem>();

            CreateMap<PaymentMethod.PaymentMethod, PaymentMethodDto>();
            CreateMap<CreateUpdatePaymentMethodDto, PaymentMethod.PaymentMethod>();

            CreateMap<CreditDebitCard, CreditDebitCardDto>();
            CreateMap<CreateUpdateCreditDebitCardDto, CreditDebitCard>();

            CreateMap<Cash, CashDto>();
            CreateMap<CreateUpdateCashDto, Cash>();
            
            CreateMap<WireTransfer, WireTransferDto>();
            CreateMap<CreateUpdateWireTransferDto, WireTransfer>();
            
            CreateMap<BankCheck, BankCheckDto>();
            CreateMap<CreateUpdateBankCheckDto, BankCheck>();
            
            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<CreateUpdateWarehouseDto, Warehouse>();

            CreateMap<ConfigurationManager.ConfigurationManager, ConfigurationManagerDto>();
            CreateMap<CreateUpdateConfigurationManagerDto, ConfigurationManager.ConfigurationManager>();

            CreateMap<Sincronizador.Sincronizador, SincronizadorDto>();
            CreateMap<CreateUpdateSincronizadorDto, Sincronizador.Sincronizador>();

            CreateMap<ProductWarehouse, ProductWarehouseDto>();
            CreateMap<CreateUpdateProductWarehouseDto, ProductWarehouse>();
        }
    }
}
