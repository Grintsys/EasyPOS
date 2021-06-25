import { FuseUtils } from '@fuse/utils';

export class SyncDto {
    id: string;
    tipoTransaccion: Transacciones;
    estado: SyncEstados;
    data: string;
    creationTime: Date;

    constructor(SyncDown?)
    {
        SyncDown = SyncDown || {};
        this.tipoTransaccion = SyncDown.tipoTransaccion || '';
        this.estado = SyncDown.estado || '';
        this.data = SyncDown.data || '';
        this.creationTime = SyncDown.creationTime || Date.now;
    }
}

export class CreateUpdateSyncDto {
    tipoTransaccion: Transacciones;
    estado: SyncEstados;
    data: string;
    creationTime: Date;

    constructor(SyncDown?)
    {
        SyncDown = SyncDown || {};
        this.tipoTransaccion = SyncDown.tipoTransaccion || '';
        this.estado = SyncDown.estado || '';
        this.data = SyncDown.data || '';
        this.creationTime = SyncDown.creationTime || Date.now;
    }
}

export enum SyncEstados {
    Fallido = 0,
    Transferido = 1,
    Creado = 3
}

export enum Transacciones {
    Orden = 0,
    NotaCredito = 1,
    NotaDebito = 3
}