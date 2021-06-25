import { FuseUtils } from '@fuse/utils';

export class SyncDto {
    tipoTransaccion: string;
    estado: string;
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