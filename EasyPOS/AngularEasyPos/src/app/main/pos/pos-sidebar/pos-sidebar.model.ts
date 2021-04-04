import { FuseUtils } from '@fuse/utils';

export class PosSidebar {
    id: number;
    subtotal: number;
    discount: number;
    tax: number;
    total: number;
    paidOut: number;
    exchange: boolean;

    /**
     * Constructor
     *
     * @param product
     */
    constructor(product?)
    {
        product = product || {};
        this.id = product.id || FuseUtils.generateGUID();
        this.subtotal = product.code || 0;
        this.discount = product.name || '';
        this.tax = product.quantity || 0;
        this.total = product.salePrice || 0;
        this.paidOut = product.total || 0;
        this.exchange = product.active || true;
    }

    /**
    * Add category
    *
    * @param {MatChipInputEvent} event
    */
    getInitialData(): PosSidebar
    {
        return new PosSidebar({id: 1, subtotal: 100, discount: 10, tax: 10, total: 100, paidOut: 500, exchange: 400});
    }
}


