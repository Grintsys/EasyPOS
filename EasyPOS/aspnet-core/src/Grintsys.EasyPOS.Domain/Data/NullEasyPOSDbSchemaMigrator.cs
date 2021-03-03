using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Grintsys.EasyPOS.Data
{
    /* This is used if database provider does't define
     * IEasyPOSDbSchemaMigrator implementation.
     */
    public class NullEasyPOSDbSchemaMigrator : IEasyPOSDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}