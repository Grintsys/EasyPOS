import { FuseUtils } from '@fuse/utils';

export class Product {
    code: number;
    productName: string;
    description: string;
    salePrice: number;
    tax: number;
    inventory: number;

    /**
     * Constructor
     *
     * @param product
     */
    constructor(product?)
    {
        product = product || {};
        this.code = product.id || FuseUtils.generateGUID();
        this.productName = product.productName || '';
        this.description = product.description || '';
        this.salePrice = product.salePrice || 0.0;
        this.tax = product.tax || 0.0;
        this.inventory = product.inventory || 0;
    }

    /**
    * Add category
    *
    * @param {MatChipInputEvent} event
    */
    getInitialData(): Product[]
    {
        return [
            new Product({productName: 'Producto1', description: 'Descripcion1',  salePrice: '24.65',  tax: '20.65', inventory: '268'}),
            new Product({productName: 'Producto2', description: 'Descripcion2',  salePrice: '24.65',  tax: '20.65',   inventory: '268'}),
            new Product({productName: 'Producto3', description: 'Descripcion3',  salePrice: '24.65',  tax: '20.65',   inventory: '268'}),
            new Product({productName: 'Producto4', description: 'Descripcion4',  salePrice: '24.65',   tax: '20.65',   inventory: '268'}),
            new Product({productName: 'Producto5', description: 'Descripcion5',  salePrice: '24.65',  tax: '20.65',   inventory: '268'}),
            new Product({productName: 'Producto6', description: 'Descripcion6',  salePrice: '24.65',  tax: '20.65',  inventory: '268'}),
            new Product({productName: 'Producto7', description: 'Descripcion7',  salePrice: '24.65',  tax: '20.65',  inventory: '268'}),
            new Product({productName: 'Producto8', description: 'Descripcion8',  salePrice: '24.65',  tax: '20.65',  inventory: '268'}),
            new Product({productName: 'Producto9', description: 'Descripcion9',  salePrice: '24.65', tax: '20.65',  inventory: '268'}),
            new Product({productName: 'Producto10', description: 'Descripcion10', salePrice: '24.65', tax: '20.65',  inventory: '268'}),
        ]
    }
}


