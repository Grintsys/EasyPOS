using Grintsys.EasyPOS.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class CreditNoteSapJob : AsyncBackgroundJob<CreaditNodeSapArgs>, ITransientDependency
    {
        private readonly ISapManager _manager;
        public CreditNoteSapJob(ISapManager sapManager)
        {
            _manager = sapManager;
        }
        public override async Task ExecuteAsync(CreaditNodeSapArgs args)
        {
            await _manager.CreateCreditNoteAsync(new CreateOrUpdateSalesOrder
            {
                CreatedDate = args.CreatedDate,
                CustomerCode = args.CustomerCode,
                CustomerName = args.CustomerName,
                SalesPersonId = args.SalesPersonId,
                WarehouseCode = args.WarehouseCode,
                Lines = args.Lines
            });
        }
    }
}
