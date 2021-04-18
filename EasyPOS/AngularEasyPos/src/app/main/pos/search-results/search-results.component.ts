import {
    Component,
    EventEmitter,
    OnDestroy,
    OnInit,
    Output,
} from "@angular/core";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";

import { FuseConfigService } from "@fuse/services/config.service";
import { ProductDto } from "app/main/products/product.model";
import { PosService } from "../pos.service";
import { OrderItemDto } from "app/main/orders/order.model";

@Component({
    selector: "search-results",
    templateUrl: "./search-results.component.html",
    styleUrls: ["./search-results.component.scss"],
})
export class SearchResultsComponent implements OnInit, OnDestroy {
    collapsed: boolean;
    fuseConfig: any;

    @Output()
    input: EventEmitter<any>;

    @Output() 
    newOrderItemEvent = new EventEmitter<OrderItemDto>();

    productList: ProductDto[];
    
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _posService: PosService
    ) {
        // Set the defaults
        this.input = new EventEmitter();
        this.collapsed = true;

        // Set the private defaults
        this._unsubscribeAll = new Subject();

        this.productList = [];
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Subscribe to config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
                this.fuseConfig = config;
            });

        this.getProductList("");
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Collapse
     */
    collapse(): void {
        this.collapsed = true;
    }

    /**
     * Search
     *
     * @param event
     */
    search(event: any): void {
        // this.input.emit(event.target.value);
        this.getProductList(event.target.value);
    }

    getProductList(filter: string) {
        this._posService.getProductList(filter).then(
            (data) => {
                this.productList = data;
            },
            (error) => {
                console.log("Search-Results-Component: Error Getting Product List " + 
                    JSON.stringify(error)
                );
            }
        );
    }

    addOrderItem(newItem: OrderItemDto) {
        this.newOrderItemEvent.emit(newItem);
    }
}
