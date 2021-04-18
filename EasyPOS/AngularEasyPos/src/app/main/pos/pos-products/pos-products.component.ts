import {
    Component,
    ElementRef,
    Input,
    OnChanges,
    SimpleChanges,
    ViewChild,
    ViewEncapsulation,
} from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";

import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";
import { MatTableDataSource } from "@angular/material/table";
import { OrderItemDto } from "app/main/orders/order.model";

@Component({
    selector: "pos-products",
    templateUrl: "./pos-products.component.html",
    styleUrls: ["./pos-products.component.scss"],
    encapsulation: ViewEncapsulation.None,
})
export class PosProductsComponent implements OnChanges {
    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "id",
        "code",
        "name",
        "quantity",
        "salePrice",
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

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.dataSource = new MatTableDataSource();
        this.orderItems = [];
    }

    /**
     * On ngAfterViewInit
     */
    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.orderItems && changes.orderItems.currentValue) 
        {
            this.dataSource = new MatTableDataSource(changes.orderItems.currentValue);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        }
    }

    increaseOrderItem(orderItemId: string){
        this.orderItems.map(x => {
            if(x.productId == orderItemId){
                console.log(x.name);
                x.quantity++; 
            }
        });
    }

    decreaseOrderItem(orderItemId: string){
        this.orderItems.map(x => {
            if(x.productId == orderItemId && x.quantity > 1){
                console.log(x.name);
                x.quantity--; 
            }
        });
    }

    removeOrderItem(orderItemId: string){
        this.orderItems = this.orderItems.filter(x => x.productId != orderItemId);
        this.dataSource = new MatTableDataSource(this.orderItems);
    }
}
