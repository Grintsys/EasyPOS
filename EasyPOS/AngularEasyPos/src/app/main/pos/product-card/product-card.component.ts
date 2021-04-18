import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Subject } from 'rxjs';

import { FuseConfigService } from '@fuse/services/config.service';
import { ProductDto } from 'app/main/products/product.model';
import { OrderItemDto } from 'app/main/orders/order.model';

@Component({
    selector   : 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls  : ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit, OnDestroy
{
    // Private
    private _unsubscribeAll: Subject<any>;
    @Input() product: ProductDto;
    @Output() newOrderItemEvent = new EventEmitter<OrderItemDto>();

    quantity: number;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     */
    constructor(
        private _fuseConfigService: FuseConfigService
    )
    {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
        this.quantity = 0;
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {

    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    increaseOrderItem(){
        this.quantity++;
    }

    decreaseOrderItem(){
        this.quantity > 0 ? this.quantity-- : 0;  
    }

    addOrderItem(){
        var orderItem = new OrderItemDto(this.product);
        orderItem.quantity = this.quantity;
        orderItem.totalItem = orderItem.quantity * orderItem.salePrice;
        this.newOrderItemEvent.emit(orderItem);
    }

}
