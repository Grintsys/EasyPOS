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

            builder.Entity<Customer.Customer>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Customers", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .IsRequired();
            });

            builder.Entity<PaymentMethod.PaymentMethod>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "PaymentMethods", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasOne(c => c.PaymentMethodType)
                    .WithMany(o => o.PaymentMethods)
                    .IsRequired();
            });

            builder.Entity<PaymentMethod.PaymentMethodType>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "PaymentMethodTypes", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Order.Order>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Orders", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(o => o.Items)
                    .WithOne(o => o.Order)
                    .IsRequired();

                //b.HasMany(o => o.DebitNotes)
                //    .WithOne(o => o.Order)
                //    .IsRequired();

                //b.HasMany(o => o.CreditNotes)
                //    .WithOne(o => o.Order)
                //    .IsRequired();

                b.HasMany(o => o.PaymentMethods)
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