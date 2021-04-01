import { FuseUtils } from '@fuse/utils';

export class CartProduct {
    id: number;
    code: string;
    name: string;
    quantity: number;
    salePrice: number;
    total: number;
    active: boolean;

    /**
     * Constructor
     *
     * @param product
     */
    constructor(product?)
    {
        product = product || {};
        this.id = product.id || FuseUtils.generateGUID();
        this.code = product.code || 0;
        this.name = product.name || '';
        this.quantity = product.quantity || 0;
        this.salePrice = product.salePrice || 0;
        this.total = product.total || 0;
        this.active = product.active || true;
    }

    /**
    * Add category
    *
    * @param {MatChipInputEvent} event
    */
    getInitialData(): CartProduct[]
    {
        return [
            new CartProduct({id: 1, code: 'P001', name: 'Producto 1', quantity: 122, salePrice: 10, total: 1021}),
            new CartProduct({id: 2, code: 'P002', name: 'Producto 2', quantity: 266, salePrice: 123, total: 1155}),
            new CartProduct({id: 3, code: 'P003', name: 'Producto 3', quantity: 345, salePrice: 132, total: 15454}),
            new CartProduct({id: 4, code: 'P004', name: 'Producto 4', quantity: 56, salePrice: 155, total: 1544}),
            new CartProduct({id: 5, code: 'P005', name: 'Producto 5', quantity: 321, salePrice: 177, total: 1454}),
            new CartProduct({id: 6, code: 'P006', name: 'Producto 6', quantity: 143, salePrice: 1456, total: 154}),
            new CartProduct({id: 7, code: 'P007', name: 'Producto 7', quantity: 133, salePrice: 1321, total: 145}),
            new CartProduct({id: 8, code: 'P008', name: 'Producto 8', quantity: 144, salePrice: 1456, total: 148}),
            new CartProduct({id: 9, code: 'P009', name: 'Producto 9', quantity: 1156, salePrice: 1555, total: 187}),
            new CartProduct({id: 10, code: 'P010', name: 'Producto 10', quantity: 1789, salePrice: 1555, total: 21}),
        ]
    }
}


