﻿using Grintsys.EasyPOS.Enums;
using System;

namespace Grintsys.EasyPOS.Sincronizador
{
    public class CreateUpdateSincronizadorDto
    {
        public Guid? TenantId { get; set; }
        public Transacciones TipoTransaccion { get; set; }
        public SyncEstados Estado { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
    }
}
