using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Grintsys.EasyPOS.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class EasyPOSMigrationsDbContextFactory : IDesignTimeDbContextFactory<EasyPOSMigrationsDbContext>
    {
        public EasyPOSMigrationsDbContext CreateDbContext(string[] args)
        {
            EasyPOSEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<EasyPOSMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new EasyPOSMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Grintsys.EasyPOS.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
