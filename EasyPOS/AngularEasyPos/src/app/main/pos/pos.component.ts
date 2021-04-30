import { Component, ElementRef, OnDestroy, OnInit, Renderer2, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { FuseTranslationLoaderService } from "@fuse/services/translation-loader.service";
import { SharedService } from "app/shared.service";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import {
    OrderDto,
    OrderItemDto,
} from "../orders/order.model";

import { locale as english } from "./i18n/en";
import { locale as spanish } from "./i18n/es";
import { PosService } from "./pos.service";

@Component({
    selector: "pos",
    templateUrl: "./pos.component.html",
    styleUrls: ["./pos.component.scss"],
})
export class PosComponent implements OnInit, OnDestroy {
    @ViewChild("searchResults") searchResults: ElementRef;
    @ViewChild("products") products: ElementRef;

    private _unsubscribeAll: Subject<any>;

    order: OrderDto;
    pageType: string;

    constructor(
        private renderer: Renderer2,
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _posService: PosService,
        private _sharedService: SharedService,
        private router: Router
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.order = new OrderDto();
        this.order.items = [];
        this._unsubscribeAll = new Subject();
        this.pageType = "Orden";
    }

    ngOnInit(): void {
        // Subscribe to update customer on changes
        this._posService.onPosChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((data) => {
                if(this.router.url == "/pos"){
                    this.pageType = "Orden";
                    this.order = new OrderDto();
                }  else if (data.Type == "nota-credito") {
                    this.getOrder(data.Id);
                    this.pageType = "Nota de Credito";
                } else if (data.Type == "nota-debito") {
                    this.getOrder(data.Id);
                    this.pageType = "Nota de Debito";
                }

                this._sharedService.updateposPageType(this.pageType);
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    search(value): void {
        this._sharedService.setPosProductsSearch(value.target.value);
    }

    getOrder(orderId: string) {
        this._posService.getOrder(orderId).then(
            (order) => {
                this.order = order;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }

    addItem(newItem: OrderItemDto) {
        var index = this.order.items.findIndex(x => x.productId == newItem.productId);
        if (index != -1) {
            this.order.items[index].quantity += newItem.quantity;
            this.order = { ...this.order };
        }
        else {
            var newArray: OrderItemDto[] = [];
            newArray.push(...this.order.items, newItem);
            this.order = { ...this.order, items: newArray };
        }
    }

    openProductList(){
        this.searchResults.nativeElement.classList.toggle("active");
    }

    updateOrderItems(items: OrderItemDto[]) {
        this.order = { ...this.order, items: items };
    }

    resetOrder(order: OrderDto) {
        this.order = order;
    }
}
