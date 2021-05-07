import { Component, OnInit } from "@angular/core";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { OrderDto, OrderType } from "../order.model";

import { locale as english } from "../i18n/en";
import { locale as spanish } from "../i18n/es";

import { OrderService } from "../order.service";
import { fuseAnimations } from "@fuse/animations";
import { Router } from "@angular/router";

@Component({
    selector: "app-order",
    templateUrl: "./order.component.html",
    styleUrls: ["./order.component.scss"],
    animations: fuseAnimations,
})
export class OrderComponent implements OnInit {
    pageType: string;
    order: OrderDto;
    isDataAvailable: boolean = false;

    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _orderService: OrderService,
        private router: Router
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.order = new OrderDto();
        // Set the default

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        this._orderService.onOrderChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "order") {
                    this._orderService.get(data.Id).then(
                        (order) => {
                            this.order = order;
                            this.isDataAvailable = true;
                            this.pageType = data.Type;
                        },
                        (error) => {
                            console.log(
                                "Get Order Failed: " + JSON.stringify(error)
                            );
                        }
                    );
                }
                else if(data.Type == 'debit-note'){
                    this._orderService.getDebitNote(data.Id).then(
                        (order) => {
                            this.order = order;
                            this.isDataAvailable = true;
                            this.pageType = data.Type;
                        },
                        (error) => {
                            console.log(
                                "Get Order Failed: " + JSON.stringify(error)
                            );
                        }
                    );
                }
            });
    }

    refreshOrder(order: OrderDto){
        this.order = order;
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    getOrderType(orderType: OrderType){
        return OrderType[orderType];
    }
}
