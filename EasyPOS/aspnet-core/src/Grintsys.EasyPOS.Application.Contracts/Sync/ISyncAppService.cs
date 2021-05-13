using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Grintsys.EasyPOS.Sync
{
    public interface ISyncAppService :
        IApplicationService
    {
        Task SyncAllAsync();
        Task SyncInboxAsync();
        Task SyncOutboxAsync();
    }
}
