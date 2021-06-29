import { FuseUtils } from '@fuse/utils';

export class SyncDto {
    id: string;
    tipoTransaccion: Transacciones;
    estado: SyncEstados;
    data: string;
    message: string;
    creationTime: Date;

    constructor(SyncDown?)
    {
        SyncDown = SyncDown || {};
        this.tipoTransaccion = SyncDown.tipoTransaccion || '';
        this.estado = SyncDown.estado || '';
        this.data = SyncDown.data || '';
        this.data = SyncDown.message || '';
        this.creationTime = SyncDown.creationTime || Date.now;
    }
}

export class CreateUpdateSyncDto {
    tipoTransaccion: Transacciones;
    estado: SyncEstados;
    data: string;
    message: string;
    creationTime: Date;

    constructor(SyncDown?)
    {
        SyncDown = SyncDown || {};
        this.tipoTransaccion = SyncDown.tipoTransaccion || '';
        this.estado = SyncDown.estado || '';
        this.data = SyncDown.data || '';
        this.data = SyncDown.message || '';
        this.creationTime = SyncDown.creationTime || Date.now;
    }
}

export enum SyncEstados {
    Fallido = 0,
    Transferido = 1,
    Creado = 2
}

export enum Transacciones {
    CreacionOrden = 0,
    CreacionNotaCredito = 1,
    CreacionNotaDebito = 3,
    SyncProductos = 4, 
    SyncClientes = 5,
    CreacionCliente = 6,
    SyncMetadata = 7
}