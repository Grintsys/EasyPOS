import { FuseUtils } from '@fuse/utils';

export class Order {
    code: number;
    customerCode: string;
    customerName: string;
    total: number;
    documentType: string;
    status: string;

    /**
     * Constructor
     *
     * @param order
     */
    constructor(order?)
    {
        order = order || {};
        this.code = order.id || FuseUtils.generateGUID();
        this.customerCode = order.customerCode || '';
        this.customerName = order.customerName || '';
        this.total = order.total || '';
        this.documentType = order.documentType || '';
        this.status = order.status || '';
    }

    /**
    * Add category
    *
    * @param {MatChipInputEvent} event
    */
    getInitialData(): Order[]
    {
        return [
            new Order({customerCode: 'C-2021-01', customerName: 'Juan Antonio Paz 1', total: '25500',  documentType: 'Credito', status: 'Cancelada'}),
            new Order({customerCode: 'C-2021-02', customerName: 'Juan Antonio Paz 2', total: '25100',  documentType: 'Contado', status: 'Creada'}),
            new Order({customerCode: 'C-2021-03', customerName: 'Juan Antonio Paz 3', total: '25800',  documentType: 'Credito', status: 'Creada'}),
            new Order({customerCode: 'C-2021-04', customerName: 'Juan Antonio Paz 4', total: '20500',  documentType: 'Contado', status: 'Cancelada'}),
            new Order({customerCode: 'C-2021-05', customerName: 'Juan Antonio Paz 5', total: '15500',  documentType: 'Credito', status: 'Sincronizada'}),
            
        ]
    }
}


