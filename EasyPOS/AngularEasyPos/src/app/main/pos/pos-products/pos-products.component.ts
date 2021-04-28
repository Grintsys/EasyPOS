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
    pageType: string;
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _sharedService: SharedService,
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.dataSource = new MatTableDataSource();
        this.orderItems = [];

        this.subscription = _sharedService.posPageType$.subscribe(
            type => {
                this.pageType = type;
            }
        );
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.orderItems && changes.orderItems.currentValue) {
            this.dataSource = new MatTableDataSource(changes.orderItems.currentValue);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        }
    }

    increaseOrderItem(orderItemId: string) {
        this.orderItems.map(x => {
            if (x.productId == orderItemId) {
                x.quantity++;
                x.totalItem = x.quantity * x.salePrice + (x.quantity * x.salePrice * x.taxes) - (x.quantity * x.salePrice * (x.discount / 100));
            }
        });

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    decreaseOrderItem(orderItemId: string) {
        this.orderItems.map(x => {
            if (x.productId == orderItemId && x.quantity > 1) {
                x.quantity--;
                x.totalItem = x.quantity * x.salePrice + (x.quantity * x.salePrice * x.taxes) - (x.quantity * x.salePrice * (x.discount / 100));
            }
        });

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    removeOrderItem(orderItemId: string) {
        this.orderItems = this.orderItems.filter(x => x.productId != orderItemId);
        this.dataSource = new MatTableDataSource(this.orderItems);

        this.newOrderItemsEvent.emit(this.orderItems);
    }

    changeDiscount(discount: string, orderItemId: string){
        this.orderItems.map(x => {
            if (x.productId == orderItemId && x.quantity > 1) {
                x.discount = Number(discount);
                x.totalItem = x.quantity * x.salePrice + (x.quantity * x.salePrice * x.taxes) - (x.quantity * x.salePrice * (x.discount / 100));
            }
        });

        this.newOrderItemsEvent.emit(this.orderItems);
    }
}
