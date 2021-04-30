import { Component, ElementRef, Input, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { OrderDto, OrderItemDto } from "../../order.model";

import { locale as english } from "../../i18n/en";
import { locale as spanish } from "../../i18n/es";
import { OrderService } from "../../order.service";
import { takeUntil } from "rxjs/operators";

@Component({
    selector: "app-order-products",
    templateUrl: "./order-products.component.html",
    styleUrls: ["./order-products.component.scss"],
})
export class OrderProductsComponent {
    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "code",
        "productName",
        "quantity",
        "salePrice",
        "discount",
        "isv",
        "total",
    ];
    orderItems: OrderItemDto[];
    pageType: string;

    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    isDataAvailable: boolean = false;

    @Input() order: OrderDto;

    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _orderService: OrderService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.orderItems = [];
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        // Subscribe to update order on changes
        this._orderService.onOrderChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "view") {
                    this.pageType = "view";
                    this.dataSource = new MatTableDataSource(this.order.items);
                    this.dataSource.paginator = this.paginator;
                    this.dataSource.sort = this.sort;
                    this.isDataAvailable = true;
                }
            });
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }
}
