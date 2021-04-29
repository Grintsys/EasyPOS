import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { OrderDto } from './main/orders/order.model';
import { ProductDto } from './main/products/product.model';

@Injectable()
export class SharedService {
    private selectedWarehouseId = new Subject<string>();
    private order = new Subject<OrderDto>();
    private posPageType = new Subject<string>();
    private productList = new Subject<ProductDto[]>();

    // Observable string streams
    selectedWarehouseId$ = this.selectedWarehouseId.asObservable();
    order$ = this.order.asObservable();
    posPageType$ = this.posPageType.asObservable();
    productList$ = this.productList.asObservable();

    // Service message commands
    updateWarehouse(warehouseId: string) {
        this.selectedWarehouseId.next(warehouseId);
    }

    updateposPageType(type: string) {
        this.posPageType.next(type);
    }

    resetOrder(){
        this.order.next(new OrderDto());
    }

    setProductList(list: ProductDto[]) {
        this.productList.next(list);
    }
}