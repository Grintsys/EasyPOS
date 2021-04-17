import { Component, Input, OnInit } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { OrderDto } from "../../order.model";
import { OrderService } from "../../order.service";

import { locale as english } from "../../i18n/en";
import { locale as spanish } from "../../i18n/es";

@Component({
    selector: "app-order-details",
    templateUrl: "./order-details.component.html",
    styleUrls: ["./order-details.component.scss"],
})
export class OrderDetailsComponent implements OnInit {
    orderForm: FormGroup;
    pageType: string;

    @Input() order: OrderDto;
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
        this.order = new OrderDto();

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    /**
     * On init
     */
    ngOnInit(): void {
        // Subscribe to update order on changes
        this._orderService.onOrderChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "view") {
                    this.pageType = "edit";
                } else {
                    this.pageType = "new";
                }
                this.orderForm = this.createorderForm();
            });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Create order form
     *
     * @returns {FormGroup}
     */
    createorderForm(): FormGroup {
        return this._formBuilder.group({
            subtotal: [{ value: this.order.subTotal, disabled: true }],
            discount: [{ value: this.order.discount, disabled: true }],
            tax: [{ value: this.order.isv, disabled: true }],
            total: [{ value: this.order.total, disabled: true }],
            toPay: [{ value: this.order.paymentAmount, disabled: true }],
            exchange: [{ value: 0, disabled: true }],
            paymentType: [{ value: 0, disabled: true }],
            customerCode: [{ value: this.order.customerCode, disabled: true }],
            customerName: [{ value: this.order.customerName, disabled: true }],
            // identity          : [{value: this.order.identity, disabled: true} ],
            // RTN               : [{value: this.order.RTN, disabled: true} ],
            // address           : [{value: this.order.address, disabled: true} ],
        });
    }
}
