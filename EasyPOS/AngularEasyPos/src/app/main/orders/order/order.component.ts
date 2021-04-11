import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { Order } from "../order.model";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";

import { OrderService } from "../order.service";
import { fuseAnimations } from "@fuse/animations";

@Component({
    selector: "app-order",
    templateUrl: "./order.component.html",
    styleUrls: ["./order.component.scss"],
    animations: fuseAnimations,
})
export class OrderComponent implements OnInit {
    order: Order;
    pageType: string;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     * @param {FuseTranslationLoaderService} _orderService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _orderService: OrderService,
        private _formBuilder: FormBuilder
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);

        // Set the default
        this.order = new Order();

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    /**
     * On init
     */
    ngOnInit(): void {}

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}
