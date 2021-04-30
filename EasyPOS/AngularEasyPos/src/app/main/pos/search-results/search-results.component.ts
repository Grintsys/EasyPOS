import {
    Component,
    EventEmitter,
    OnDestroy,
    OnInit,
    Output,
} from "@angular/core";
import { Subject, Subscription } from "rxjs";
import { takeUntil } from "rxjs/operators";

import { FuseConfigService } from "@fuse/services/config.service";
import { ProductDto } from "app/main/products/product.model";
import { PosService } from "../pos.service";
import { OrderItemDto } from "app/main/orders/order.model";
import { SharedService } from "app/shared.service";

@Component({
    selector: "search-results",
    templateUrl: "./search-results.component.html",
    styleUrls: ["./search-results.component.scss"],
})
export class SearchResultsComponent implements OnInit, OnDestroy {
    @Output()
    input: EventEmitter<any>;

    @Output()
    newOrderItemEvent = new EventEmitter<OrderItemDto>();

    fuseConfig: any;
    collapsed: boolean;
    selectedWarehouseId: string;
    productList: ProductDto[];
    subscription: Subscription;
    searchFilter: string;

    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseConfigService: FuseConfigService,
        private _posService: PosService,
        private _sharedService: SharedService
    ) {
        this._unsubscribeAll = new Subject();

        this.input = new EventEmitter();

        this.collapsed = true;
        this.productList = [];

        this.subscription = _sharedService.selectedWarehouseId$.subscribe(
            () => {
                this.getProductList("");
            }
        );

        this.subscription = _sharedService.posProductsSearch$.subscribe(
            data => {
                data != undefined ? this.searchFilter = data : this.searchFilter = '';
                this.getProductList(this.searchFilter);
            }
        );
    }

    ngOnInit(): void {
        // Subscribe to config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
                this.fuseConfig = config;
            });

        this.getProductList("");
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    collapse(): void {
        this.collapsed = true;
    }

    search(event: any): void {
        // this.input.emit(event.target.value);
        this.getProductList(event.target.value);
    }

    getProductList(filter: string) {
        this._posService.getProductList(filter).then(
            (data) => {
                this.productList = data;
                this._sharedService.setProductList(this.productList);
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
