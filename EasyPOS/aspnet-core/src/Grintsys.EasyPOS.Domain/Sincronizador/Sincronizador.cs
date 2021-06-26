using Grintsys.EasyPOS.Enums;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class Sincronizador : FullAuditedAggregateRoot<Guid>
    {
        public Transacciones TipoTransaccion { get; set; }
        public SyncEstados Estado { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
    }
}
