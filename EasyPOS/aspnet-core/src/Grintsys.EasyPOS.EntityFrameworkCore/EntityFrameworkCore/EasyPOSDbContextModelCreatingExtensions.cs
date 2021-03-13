using Grintsys.EasyPOS.Models;
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

            builder.Entity<Product>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Products", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Customer>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Customers", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .IsRequired();
            });

            builder.Entity<Order>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "Orders", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasMany(o => o.OrderItems)
                    .WithOne(o => o.Order)
                    .IsRequired();
            });

            builder.Entity<OrderItem>(b =>
            {
                b.ToTable(EasyPOSConsts.DbTablePrefix + "OrderItems", EasyPOSConsts.DbSchema);
                b.ConfigureByConvention();
            });
        }
    }
}