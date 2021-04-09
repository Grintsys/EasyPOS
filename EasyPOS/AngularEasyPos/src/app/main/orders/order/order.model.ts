import { FuseUtils } from '@fuse/utils';

export class Order {
    code: number;
    subtotal: number;
    discount: number;
    tax: number;
    total: number;
    toPay: number;
    exchange: number;
    paymentType: string;
    customerCode: string;
    customerName: string;
    identity: string;
    RTN: string;
    address: string;
    documentType: string;
    status: string;
    products: Array<OrderProduct>;
    creditAndDebitNotes: Array<CreditDebitNote>;

    /**
     * Constructor
     *
     * @param order
    */
    constructor(order?)
    {
        order = order || {};
        this.code = order.code || FuseUtils.generateGUID();
        this.subtotal = order.subtotal || '';
        this.discount = order.discount || '';
        this.tax = order.tax || '';
        this.total = order.total || '';
        this.toPay = order.toPay || '';
        this.exchange = order.exchange || '';
        this.paymentType = order.paymentType || '';
        this.customerCode = order.customerCode || '';
        this.customerName = order.customerName || '';
        this.identity = order.identity || '';
        this.RTN = order.RTN || '';
        this.address = order.address || '';
        this.documentType = order.documentType || '';
        this.status = order.status || '';
        this.products = order.prducts || null;
        this.creditAndDebitNotes = order.creditAndDebitNotes || null;
    }

    /**
    * Add category
    *
    * @param {Order} event
    */
    getInitialData(): Order[]
    {
        return [
            new Order({
                subtotal: 100,
                discount: -10,
                tax: 10,
                total: 100,
                toPay: 500,
                exchange: 400,
                paymentType: 'Tarjeta de credito',
                customerCode: 'C-2021-01',
                customerName: 'Juan Antonio Paz',
                identity: '1614188500185',
                RTN: '16141885001850',
                address: 'San Pedro Sula',
                documentType: 'Credito',
                status: 'Cancelada',
            }),

            new Order({
                subtotal: 100,
                discount: -10,
                tax: 10,
                total: 100,
                toPay: 500,
                exchange: 400,
                paymentType: 'Tarjeta de débito',
                customerCode: 'C-2021-01',
                customerName: 'Juan Mjía Paz',
                identity: '1614188500185',
                RTN: '16141885001850',
                address: 'San Pedro Sula',
                documentType: 'Contado',
                status: 'Creada',
            }),

            new Order({
                subtotal: 100,
                discount: -10,
                tax: 10,
                total: 100,
                toPay: 500,
                exchange: 400,
                paymentType: 'Tarjeta de credito',
                customerCode: 'C-2021-01',
                customerName: 'Juan Antonio Paz',
                identity: '1614188500185',
                RTN: '16141885001850',
                address: 'San Pedro Sula',
                documentType: 'Contado',
                status: 'Sincronizada',
            }),

        ]
    }
}

export class OrderProduct {
    code: number;
    productName: string;
    quantity: string;
    salePrice: string;
    total: number;

    /**
     * Constructor
     *
     * @param orderProduct
    */
     constructor(orderProduct?)
     {
        orderProduct = orderProduct || {};
        this.code = orderProduct.code || FuseUtils.generateGUID();
        this.productName = orderProduct.productName || '';
        this.quantity = orderProduct.quantity || 0;
        this.salePrice = orderProduct.salePrice || 0;
        this.total = orderProduct.total || 0;
     }

    /**
    * Get data temp
    *
    * @param {OrderProduct} event
    */
    getInitialData(): OrderProduct[]
    {
        return [
            new OrderProduct({productName: 'Via Delantera', quantity: 10,  salePrice: 500, total: 500}),
            new OrderProduct({productName: 'Via Delantera', quantity: 20,  salePrice: 1000, total: 1000}),
            new OrderProduct({productName: 'Via Delantera', quantity: 5,  salePrice: 250, total: 250}),
        ]
    }
}

export class CreditDebitNote {
    code: number;
    customerCode: string;
    customerName: string;
    total: number;
    documentType: string;
    status: string;

    /**
     * Constructor
     *
     * @param creditDebitNote
    */
     constructor(creditDebitNote?)
     {
        creditDebitNote = creditDebitNote || {};
        this.code = creditDebitNote.code || FuseUtils.generateGUID();
        this.customerCode = creditDebitNote.customerCode || '';
        this.customerName = creditDebitNote.customerName || '';
        this.total = creditDebitNote.total || 0;
        this.documentType = creditDebitNote.documentType || '';
        this.status = creditDebitNote.status || '';
     }

    /**
    * Get data temp
    *
    * @param {CreditDebitNote} event
    */
    getInitialData(): CreditDebitNote[]
    {
        return [
            new CreditDebitNote({customerCode: 'C-2021-01', customerName: 'Juan Antonio Paz', total: 25500,  documentType: 'Crédito', status: 'Sincronizada'}),
            new CreditDebitNote({customerCode: 'C-2021-01', customerName: 'Juan Antonio Paz', total: 20500,  documentType: 'Débito', status: 'Sincronizada'}),
        ]
    }
}


