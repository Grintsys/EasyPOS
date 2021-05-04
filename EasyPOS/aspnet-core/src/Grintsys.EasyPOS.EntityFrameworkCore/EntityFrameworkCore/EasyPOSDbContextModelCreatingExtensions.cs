using Grintsys.EasyPOS.CreditNote;
using Grintsys.EasyPOS.DebitNote;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Grintsys.EasyPOS.EntityFrameworkCore
{
    public static class EasyPOSDbContextModelCreatingExtensions
    {
        public static void ConfigureEasyPOS(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            builder.Entity<Product.Product>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Products", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Product.Warehouse>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Warehouses", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Product.ProductWarehouse>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "ProductWarehouses", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasOne(p => p.Product)
                    .WithMany(p => p.ProductWarehouse);

                b.HasOne(p => p.Warehouse)
                    .WithMany(p => p.ProductWarehouses);
            });

            builder.Entity<Customer.Customer>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Customers", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            builder.Entity<PaymentMethod.PaymentMethod>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "PaymentMethods", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(p => p.BankChecks)
                    .WithOne(p => p.PaymentMethod);
                
                b.HasOne(o => o.Cash)
                    .WithOne(o => o.PaymentMethod);
                
                b.HasOne(o => o.WireTransfer)
                    .WithOne(o => o.PaymentMethod);
                
                b.HasOne(o => o.CreditDebitCard)
                    .WithOne(o => o.PaymentMethod);
            });
            
            builder.Entity<Order.Order>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Orders", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(o => o.Items)
                    .WithOne(o => o.Order)
                    .IsRequired();
                
                b.HasMany(o => o.CreditNotes)
                    .WithOne(o => o.Order)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne(o => o.PaymentMethods)
                    .WithOne(o => o.Order)
                    .IsRequired();
            });
            
            builder.Entity<Order.OrderItem>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "OrderItems", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<CreditNote.CreditNote>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "CreditNotes", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(o => o.Items)
                    .WithOne(o => o.CreditNote)
                    .IsRequired();
            });

            builder.Entity<CreditNoteItem>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "CreditNoteItems", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<DebitNote.DebitNote>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "DebitNotes", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(o => o.Items)
                    .WithOne(o => o.DebitNote)
                    .IsRequired();
            });

            builder.Entity<DebitNoteItem>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "DebitNoteItems", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });
        }
    }
}