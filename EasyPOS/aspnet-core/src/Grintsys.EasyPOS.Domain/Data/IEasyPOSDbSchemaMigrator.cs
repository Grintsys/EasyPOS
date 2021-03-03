using System.Threading.Tasks;

namespace Grintsys.EasyPOS.Data
{
    public interface IEasyPOSDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
