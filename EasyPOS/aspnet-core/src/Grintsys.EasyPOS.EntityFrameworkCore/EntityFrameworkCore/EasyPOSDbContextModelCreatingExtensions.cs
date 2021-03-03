using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Grintsys.EasyPOS.EntityFrameworkCore
{
    public static class EasyPOSDbContextModelCreatingExtensions
    {
        public static void ConfigureEasyPOS(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(EasyPOSConsts.DbTablePrefix + "YourEntities", EasyPOSConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}