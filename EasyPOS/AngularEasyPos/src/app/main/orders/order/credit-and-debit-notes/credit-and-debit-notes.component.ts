import { Component, ElementRef, Input, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";

import { locale as english } from "../../i18n/en";
import { locale as spanish } from "../../i18n/es";
import { CreditDebitNote, OrderDto } from "../../order.model";
import { OrderService } from "../../order.service";

@Component({
    selector: "app-credit-and-debit-notes",
    templateUrl: "./credit-and-debit-notes.component.html",
    styleUrls: ["./credit-and-debit-notes.component.scss"],
})
export class CreditAndDebitNotesComponent {
    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "code",
        "customerCode",
        "customerName",
        "total",
        "documentType",
        "status",
        "options",
    ];

    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    @Input() order: OrderDto;
    notes: any[];
    pageType: string;

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
        this.order = new OrderDto();
        this.notes = [];
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    /**
     * On ngAfterViewInit
     */
    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    ngOnInit(): void {
        // Subscribe to update order on changes
        this._orderService.onOrderChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "view") {
                    this.pageType = "view";
                    this.setDataSource();
                }
            });
    }

    setDataSource() {
        this.order.creditNotes.forEach((cn) => {
            var creditNote = new CreditDebitNote(cn);
            creditNote.documentType = "Note de Credito";
            this.notes.push(creditNote);
        });

        this.order.debitNotes.forEach((dn) => {
            var debitNote = new CreditDebitNote(dn);
            debitNote.documentType = "Note de Debito";
            this.notes.push(debitNote);
        });

        this.dataSource = new MatTableDataSource(this.notes);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    delete(id: string, type: string){
        if(type === 'Note de Debito'){
            this._orderService.deleteDebitNote(id).then(
                (d) => {
                    
                },
                (error) => {
                    console.log("Promise rejected with " + JSON.stringify(error));
                }
            );
        }
        else if(type == 'Note de Credito'){
            this._orderService.deleteCreditNote(id).then(
                (d) => {
                    
                },
                (error) => {
                    console.log("Promise rejected with " + JSON.stringify(error));
                }
            );
        }
    }
}
