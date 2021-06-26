using Grintsys.EasyPOS.Enums;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class CreateUpdateSincronizadorDto
    {
        public Transacciones TipoTransaccion { get; set; }
        public SyncEstados Estado { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
    }
}
