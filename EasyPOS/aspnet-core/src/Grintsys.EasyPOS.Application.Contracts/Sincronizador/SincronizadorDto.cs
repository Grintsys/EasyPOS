using Grintsys.EasyPOS.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class SincronizadorDto : FullAuditedEntityDto<Guid>
    {
        public Transacciones TipoTransaccion { get; set; }
        public SyncEstados Estado { get; set; }
        public string Data { get; set; }
    }
}
