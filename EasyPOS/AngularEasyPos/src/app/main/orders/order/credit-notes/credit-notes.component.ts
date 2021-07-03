import { Component, ElementRef, Input, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { Output, EventEmitter } from '@angular/core';
import { locale as english } from "../../i18n/en";
import { locale as spanish } from "../../i18n/es";
import { DocumentState, OrderDto } from "../../order.model";
import { OrderService } from "../../order.service";

@Component({
    selector: "app-credit-notes",
    templateUrl: "./credit-notes.component.html",
    styleUrls: ["./credit-notes.component.scss"],
})
export class CreditNotesComponent {
    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "code",
        "customerCode",
        "customerName",
        "total",
        "status",
        "options",
    ];

    @Output() orderEvent = new EventEmitter<OrderDto>();

    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    @Input() order: OrderDto;
    pageType: string;
    currency: string= '';
    
    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _orderService: OrderService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.order = new OrderDto();
        this._unsubscribeAll = new Subject();
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    ngOnInit(): void {
        // Subscribe to update order on changes
        this._orderService.onOrderChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "order") {
                    this.pageType = data.Type;
                    this.setDataSource();
                    this.getConfigList('Moneda');
                }
            });
    }

    setDataSource() {
        this.dataSource = new MatTableDataSource(this.order.creditNotes);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    getConfigList(filter: string) {
        this._orderService.getConfList(filter).then(
            (d) => {
                this.currency = JSON.parse(d[0].value).Currency;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    delete(id: string) {
        this._orderService.deleteCreditNote(id).then(
            (d) => {
                this.get();
            },
            (error) => {
                console.log("Delete Credit Note Failed: " + JSON.stringify(error));
            }
        );
    }

    get() {
        this._orderService.get(this.order.id).then(
            (order) => {
                this.order = order;
                this.setDataSource();
                this.orderEvent.emit(order);
            },
            (error) => {
                console.log(
                    "Get Order Faild: " + JSON.stringify(error)
                );
            }
        );
    }

    getDocumentState(documentState: DocumentState) {
        return DocumentState[documentState];
    }
}
