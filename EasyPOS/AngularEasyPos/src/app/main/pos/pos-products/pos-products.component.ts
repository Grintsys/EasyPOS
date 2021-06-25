import {
    Component,
    ElementRef,
    Input,
    OnChanges,
    Output,
    SimpleChanges,
    ViewChild,
    ViewEncapsulation,
    EventEmitter
} from "@angular/core";

import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";

import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";
import { MatTableDataSource } from "@angular/material/table";
import { OrderItemDto } from "app/main/orders/order.model";
import { SharedService } from "app/shared.service";
import { Subscription } from "rxjs";
import { ProductDto } from "app/main/products/product.model";

@Component({
    selector: "pos-products",
    templateUrl: "./pos-products.component.html",
    styleUrls: ["./pos-products.component.scss"],
    encapsulation: ViewEncapsulation.None,
})
export class PosProductsComponent implements OnChanges {
    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "code",
        "name",
        "quantity",
        "salePrice",
        "tax",
        "discount",
        "total",
        "options",
    ];

    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    @Input() orderItems: OrderItemDto[];
    @Output() newOrderItemsEvent = new EventEmitter<OrderItemDto[]>();

    subscription: Subscription;
    productList: ProductDto[];
    pageType: string;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _sharedService: SharedService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.dataSource = new MatTableDataSource();
        this.orderItems = [];

        this.subscription = _sharedService.posPageType$.subscribe(
            type => {
                this.pageType = type;
            }
        );

        this.subscription = _sharedService.productList$.subscribe(
            list => {
                this.productList = list;
            }
        );
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.orderItems && changes.orderItems.currentValue) {

            this.orderItems = changes.orderItems.currentValue.map(x => {
                var prodIndex = this.productList.findIndex(y => y.id == x.productId);
                
                if(x.quantity > this.productList[prodIndex].inventory){
                    x.quantity = this.productList[prodIndex].inventory;
                }

                return x;
            })
            this.dataSource = new MatTableDataSource(this.orderItems);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        }
    }

    increaseOrderItem(orderItemId: string) {
        var index = this.productList.findIndex(x => x.id == orderItemId);
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (this.productList[index].inventory > this.orderItems[orderItemIndex].quantity) {
            this.orderItems[orderItemIndex].quantity++;
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    decreaseOrderItem(orderItemId: string) {
        this.orderItems.map(x => {
            if (x.productId == orderItemId && x.quantity > 1) {
                x.quantity--;
                x.totalItem = this.calculateTotalItem(x);
            }
        });

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    increaseDiscount(orderItemId: string) {
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (this.orderItems[orderItemIndex].discount < 5) {
            this.orderItems[orderItemIndex].discount++;
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    decreaseDiscount(orderItemId: string) {
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (this.orderItems[orderItemIndex].discount > 0) {
            this.orderItems[orderItemIndex].discount--;
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    removeOrderItem(orderItemId: string) {
        this.orderItems = this.orderItems.filter(x => x.productId != orderItemId);
        this.dataSource = new MatTableDataSource(this.orderItems);

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    changeDiscount(discount: string, orderItemId: string) {
        if (Number(discount) > 0 && Number(discount) <= 100) {
            this.orderItems.map(x => {
                if (x.productId == orderItemId && x.quantity > 1) {
                    x.discount = Number(discount);
                    x.totalItem = this.calculateTotalItem(x);
                }
            });

            this.newOrderItemsEvent.emit(this.orderItems);
        }
    }

    changeQuantity(quantity: string, orderItemId: string) {
        var index = this.productList.findIndex(x => x.id == orderItemId);
        var orderItemIndex = this.orderItems.findIndex(x => x.productId == orderItemId);

        if (Number(quantity) > 0 && this.productList[index].inventory >= Number(quantity)) {
            this.orderItems[orderItemIndex].quantity = Number(quantity);
            this.orderItems[orderItemIndex].totalItem = this.calculateTotalItem(this.orderItems[orderItemIndex]);
        }

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    calculateTotalItem(x: OrderItemDto) {
        return x.quantity * x.salePrice + (x.quantity * x.salePrice) - (x.quantity * x.salePrice * (x.discount / 100));
    }
}
