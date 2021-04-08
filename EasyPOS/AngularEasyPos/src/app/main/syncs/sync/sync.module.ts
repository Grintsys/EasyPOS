import { FuseUtils } from '@fuse/utils';

export class SyncDown {
    code: number;
    informationType: string;
    status: string;

    /**
     * Constructor
     *
     * @param SyncDown
     */
    constructor(SyncDown?)
    {
        SyncDown = SyncDown || {};
        this.code = SyncDown.id || FuseUtils.generateGUID();
        this.informationType = SyncDown.informationType || '';
        this.status = SyncDown.status || '';
    }

    /**
    * Add category
    */
    getInitialData(): SyncDown[]
    {
        return [
            new SyncDown({informationType: 'Information1', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information2', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information3', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information4', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information5', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information6', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information7', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information8', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information9', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information10', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information11', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information12', status: 'Sincronizado'}),
            new SyncDown({informationType: 'Information13', status: 'Sincronizado'}),
        ]
    }
}


export class SyncUp {
    code: number;
    customerCode: string;
    customerName: string;
    total: number;
    documentType: string;
    status: string;

    /**
     * Constructor
     *
     * @param SyncUp
     */
    constructor(SyncUp?)
    {
        SyncUp = SyncUp || {};
        this.code = SyncUp.id || FuseUtils.generateGUID();
        this.customerCode = SyncUp.customerCode || '';
        this.customerName = SyncUp.customerName || '';
        this.total = SyncUp.total || 0.0;
        this.documentType = SyncUp.documentType || '';
        this.status = SyncUp.status || '';
    }

    /**
    * Add category
    *
    * @param {MatChipInputEvent} event
    */
    getInitialData(): SyncUp[]
    {
        return [
            new SyncUp({customerCode: 'Information1',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 1', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information2',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 2', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information3',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 3', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Informaton4',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 4', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information5',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 5', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information6',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 6', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information7',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 7', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information8',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 8', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information9',  customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 9', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information10', customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 10', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information11', customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 11', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information12', customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 12', status: 'Sincronizado'}),
            new SyncUp({customerCode: 'Information13', customerName: 'Sincronizado', total: 25.0, documentType: 'tipo documento 13', status: 'Sincronizado'}),
        ]
    }
}


