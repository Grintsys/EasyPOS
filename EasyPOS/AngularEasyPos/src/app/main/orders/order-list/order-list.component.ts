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
        "creationTime",
        "documentType",
        "customerCode",
        "customerName",
        "subTotal",
        "isv",
        "discount",
        "total",
        "state",
        "options",
    ];
    orders: OrderDto[];
    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _orderService: OrderService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.orders = new Array;
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngAfterViewInit() {
        this.getOrderList('');
    }

    search(value): void {
        if (value.target != undefined && value.target.value != undefined){
            this.getOrderList(value.target.value);
        }
    }

    getOrderList(filter: string) {
        var dataList: any[] = [];

        this._orderService.getList(filter).then(
            (d) => {
                var orderList: any[] = d;
                orderList.map(x => {
                    x.documentType = 'Orden';
                    x.url = 'order';
                });
                dataList = dataList.concat(orderList);

                this._orderService.getDebitNoteList().then(
                    (d) => {
                        var debitNoteList: any[] = d;
                        debitNoteList.map(x => {
                            x.documentType = 'Nota de Debito';
                            x.url = 'debit-note';
                        });
                        dataList = dataList.concat(debitNoteList);

                        this.dataSource = new MatTableDataSource(dataList);
                        this.dataSource.paginator = this.paginator;
                        this.dataSource.sort = this.sort;
                    },
                    (error) => {
                        console.log("Promise rejected with " + JSON.stringify(error));
                    }
                );
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

    getOrderType(orderType: OrderType) {
        return OrderType[orderType];
    }

    getDocumentState(documentState: DocumentState) {
        return DocumentState[documentState];
    }
}
