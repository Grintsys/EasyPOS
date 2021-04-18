import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { OrderDto, OrderItemDto } from '../orders/order.model';

import { locale as english } from './i18n/en';
import { locale as spanish } from './i18n/es';
import { PosService } from './pos.service';


@Component({
    selector   : 'pos',
    templateUrl: './pos.component.html',
    styleUrls  : ['./pos.component.scss']
})
export class PosComponent
{
    @ViewChild("searchResults") searchResults: ElementRef;
    @ViewChild("products") products: ElementRef;

    order: OrderDto;
    orderItems: OrderItemDto[];
    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private renderer: Renderer2,
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _posService: PosService
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.order = new OrderDto();
        this.orderItems = [];
    }

    /**
     * Search
     *
     * @param value
     */
    search(value): void
    {
        // Do your search here...
        console.log(value);
        this.searchResults.nativeElement.classList.toggle("active");
    }

    addItem(newItem: OrderItemDto) {
        var newArray: OrderItemDto[] = [];
        newArray.push(...this.order.items, newItem);
        this.order.items = newArray;
    }
}
