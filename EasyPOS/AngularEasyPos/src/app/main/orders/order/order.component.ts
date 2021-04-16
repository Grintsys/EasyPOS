import { Component, OnInit } from "@angular/core";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { OrderDto } from "../order.model";

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
    pageType: string;
    order: OrderDto;
    isDataAvailable: boolean = false;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     * @param {FuseTranslationLoaderService} _orderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _orderService: OrderService
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.order = new OrderDto();
        // Set the default

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    /**
     * On init
     */
    ngOnInit(): void {
        this._orderService.onOrderChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if (data.Type == "view") {
                    this._orderService.get(data.Id).then(
                        (order) => {
                            this.order = order;
                            this.isDataAvailable = true;
                            this.pageType = "view";
                        },
                        (error) => {
                            console.log(
                                "Promise rejected with " + JSON.stringify(error)
                            );
                        }
                    );
                }
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
}
