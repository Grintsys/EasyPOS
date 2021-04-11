import {
    Component,
    ElementRef,
    ViewChild,
    ViewEncapsulation,
    OnInit,
} from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";

import { fuseAnimations } from "@fuse/animations";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";

import { CustomerDto } from "../customer.model";
import { CustomerService } from "../customer.service";

import { Subject } from "rxjs";

@Component({
    selector: "app-customer-list",
    templateUrl: "./customer-list.component.html",
    styleUrls: ["./customer-list.component.scss"],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
})
export class CustomerListComponent {
    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "code",
        "firstName",
        "lastName",
        "idNumber",
        "rtn",
        "address",
        "phoneNumber",
        "status",
        "options",
    ];
    customer: CustomerDto;
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _customerService: CustomerService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {}
    /**
     * On ngAfterViewInit
     */
    ngAfterViewInit() {
        this.getCustomerList('');
    }

    /**
     * Search
     *
     * @param value
     */
    search(value): void {
        this.getCustomerList(value.target.value);
    }

    getCustomerList(filter: string) {
        this._customerService.getList(filter).then(
            (d) => {
                this.dataSource = new MatTableDataSource(d);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    deleteCustomer(id: string){
        this._customerService.delete(id).then(
            (d) => {
                this.getCustomerList('');
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }
}
