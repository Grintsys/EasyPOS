import { Component, Input, OnInit } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { OrderDto, OrderType } from "../../order.model";
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
    currency: string= '';

    @Input() order: OrderDto;
    // Private
    private _unsubscribeAll: Subject<any>;

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

    ngOnInit(): void {
        // Subscribe to update order on changes
        this._orderService.onOrderChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                this.pageType = data.Type;
                this.orderForm = this.createorderForm();
                this.getConfigList('Moneda');
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    createorderForm(): FormGroup {
        return this._formBuilder.group({
            subtotal: [{ value: this.order.subTotal, disabled: true }],
            discount: [{ value: this.order.discount, disabled: true }],
            orderType: [{ value: this.getOrderType(this.order.orderType), disabled: true }],
            tax: [{ value:  this.order.isv, disabled: true }],
            total: [{ value: this.order.total, disabled: true }],
            toPay: [{ value: this.order.paymentAmount, disabled: true }],
            customerCode: [{ value: this.order.customerCode, disabled: true }],
            customerName: [{ value: this.order.customerName, disabled: true }]
        });
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

    getOrderType(orderType: OrderType){
        return OrderType[orderType];
    }
}
