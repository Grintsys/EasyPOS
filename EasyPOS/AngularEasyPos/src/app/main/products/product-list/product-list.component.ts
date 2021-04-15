import {
    Component,
    ElementRef,
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
import { ProductService } from "../product.service";
import { ProductDto } from "../product.model";

@Component({
    selector: "app-product-list",
    templateUrl: "./product-list.component.html",
    styleUrls: ["./product-list.component.scss"],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
})
export class ProductListComponent {
    @ViewChild(MatPaginator, { static: true })
    paginator: MatPaginator;

    @ViewChild(MatSort, { static: true })
    sort: MatSort;

    @ViewChild("filter", { static: true })
    filter: ElementRef;

    dataSource = new MatTableDataSource();
    displayedColumns: string[] = [
        "code",
        "name",
        "description",
        "salePrice",
        "taxes",
        "inventory",
    ];

    product: ProductDto;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _productService: ProductService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    /**
     * On ngAfterViewInit
     */
    ngAfterViewInit() {
        this.getProductList('');
    }

    /**
     * Search
     *
     * @param value
     */
    search(value): void {
        this.getProductList(value.target.value);
    }

    getProductList(filter: string) {
        this._productService.getList(filter).then(
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
}
