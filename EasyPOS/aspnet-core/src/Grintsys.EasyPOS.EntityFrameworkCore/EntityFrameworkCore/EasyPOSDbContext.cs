using Grintsys.EasyPOS.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Grintsys.EasyPOS.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See EasyPOSMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class EasyPOSDbContext : AbpDbContext<EasyPOSDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Customer.Customer> Customers { get; set; }
        public DbSet<Product.Product> Products { get; set; }
        public DbSet<Product.Warehouse> Warehouses { get; set; }
        public DbSet<Product.ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Order.Order> Orders { get; set; }
        public DbSet<CreditNote.CreditNote> CreditNotes { get; set; }
        public DbSet<DebitNote.DebitNote> DebitNotes { get; set; }
        public DbSet<Order.OrderItem> OrderItems { get; set; }
        public DbSet<CreditNote.CreditNoteItem> CreditNoteItems { get; set; }
        public DbSet<DebitNote.DebitNoteItem> DebitNoteItems { get; set; }
        public DbSet<PaymentMethod.PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethod.BankCheck> BankChecks { get; set; }
        public DbSet<PaymentMethod.CreditDebitCard> CreditDebitCards { get; set; }
        public DbSet<PaymentMethod.WireTransfer> WireTransfers { get; set; }
        public DbSet<PaymentMethod.Cash> Cash { get; set; }
        public DbSet<ConfigurationManager.ConfigurationManager> ConfigurationManager { get; set; }
        public DbSet<Sincronizador.Sincronizador> Sincronizador { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside EasyPOSDbContextModelCreatingExtensions.ConfigureEasyPOS
         */

        public EasyPOSDbContext(DbContextOptions<EasyPOSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the EasyPOSEfCoreEntityExtensionMappings class
                 */
            });

            builder.Entity<AppUser>()
                .Property(e => e.SalesPersonCode)
                .HasDefaultValue(1);

            /* Configure your own tables/entities inside the ConfigureEasyPOS method */

            builder.ConfigureEasyPOS();
        }
    }
}
