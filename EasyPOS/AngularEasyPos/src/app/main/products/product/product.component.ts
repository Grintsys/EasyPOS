import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { fuseAnimations } from "@fuse/animations";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { ProductDto } from "../product.model";
import { ProductService } from "../product.service";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";

@Component({
    selector: "app-product",
    templateUrl: "./product.component.html",
    styleUrls: ["./product.component.scss"],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None,
})
export class ProductComponent implements OnInit, OnDestroy {
    product: ProductDto;
    pageType: string;
    productForm: FormGroup;

    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _productService: ProductService,
        private _formBuilder: FormBuilder
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        // Set the default
        this.product = new ProductDto();
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        // Subscribe to update product on changes
        this._productService.onProductChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == 'view') {
                    this.getProductById(data.Id);
                    this.pageType = 'view';
                    this.productForm = this.createProductForm();
                }
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    createProductForm(): FormGroup {
        return this._formBuilder.group({
            code: [{ value: this.product.code, disabled: true }],
            name: [{ value: this.product.name, disabled: true }],
            description: [{ value: this.product.description, disabled: true }],
            salePrice: [{ value: this.product.salePrice, disabled: true }],
            taxes: [{ value: this.product.taxes, disabled: true }],
            inventory: [{ value: this.product.inventory, disabled: true }],
        });
    }

    getProductById(id: string) {
        this._productService.get(id).then(
            (product) => {
                this.product = product;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }
}
