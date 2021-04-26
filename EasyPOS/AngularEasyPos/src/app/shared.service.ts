import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { OrderDto } from './main/orders/order.model';

@Injectable()
export class SharedService {
    private selectedWarehouseId = new Subject<string>();
    private order = new Subject<OrderDto>();

    // Observable string streams
    selectedWarehouseId$ = this.selectedWarehouseId.asObservable();
    order$ = this.order.asObservable();

    // Service message commands
    updateWarehouse(warehouseId: string) {
        this.selectedWarehouseId.next(warehouseId);
    }

    resetOrder(){
        this.order.next(new OrderDto());
    }
}