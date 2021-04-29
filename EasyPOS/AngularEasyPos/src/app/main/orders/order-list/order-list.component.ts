import {
    Component,
    ElementRef,
    OnInit,
    ViewChild,
    ViewEncapsulation,
} from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { fuseAnimations } from "@fuse/animations";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";
import { DocumentState, OrderDto, OrderType } from "../order.model";
import { OrderService } from "../order.service";

@Component({
    selector: "app-order-list",
    templateUrl: "./order-list.component.html",
    styleUrls: ["./order-list.component.scss"],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
})
export class OrderListComponent {
    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "customerCode",
        "customerName",
        "subTotal",
        "isv",
        "discount",
        "total",
        "state",
        "orderType",
        "options",
    ];
    orders: OrderDto[];
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _orderService: OrderService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.orders = new Array;
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    /**
     * On ngAfterViewInit
     */
    ngAfterViewInit() {
        this.getOrderList('');
    }

    /**
     * Search
     *
     * @param value
     */
    search(value): void {
        this.getOrderList(value.target.value);
    }

    getOrderList(filter: string) {
        this._orderService.getList(filter).then(
            (d) => {
                // d.forEach(e => {
                //   this.orders.push(new OrderDto(e));
                // });
                this.dataSource = new MatTableDataSource(d);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    deleteOrder(id: string) {
        this._orderService.delete(id).then(
            (d) => {
                this.getOrderList('');
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    getOrderType(orderType: OrderType){
        return OrderType[orderType];
    }

    getDocumentState(documentState: DocumentState){
        return DocumentState[documentState];
    }
}
