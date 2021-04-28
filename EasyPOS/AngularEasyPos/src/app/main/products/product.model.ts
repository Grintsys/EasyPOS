export class CreateUpdateProductDto {
    name?: string;
    description?: string;
    code?: string;
    salePrice: number;
    taxes: number;
    isActive: boolean;
    imageUrl?: string;
    productWarehouses: CreateUpdateProductWarehouseDto[];
}

export class CreateUpdateProductWarehouseDto {
    productId?: string;
    warehouseId?: string;
    inventory: number;
}

export class CreateUpdateWarehouseDto {
    name?: string;
    address?: string;
}

export class ProductDto{
    id?: string;
    name?: string;
    description?: string;
    code?: string;
    salePrice: number;
    taxes: number;
    isActive: boolean;
    imageUrl?: string;
    inventory: number;
    productWarehouse: ProductWarehouseDto[];
}

export class ProductLookupDto {
    id?: string;
    name?: string;
}

export class ProductWarehouseDto{
    id?: string;
    productId?: string;
    productName?: string;
    warehouseId?: string;
    warehouseName?: string;
    inventory: number;
}

export class WarehouseDto{
    id?: string;
    name?: string;
    address?: string;
    productWarehouses: ProductWarehouseDto[];
}