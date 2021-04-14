import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from './i18n/en';
import { locale as spanish } from './i18n/es';


@Component({
    selector   : 'pos',
    templateUrl: './pos.component.html',
    styleUrls  : ['./pos.component.scss']
})
export class PosComponent
{

    @ViewChild("searchResults") searchResults: ElementRef;
    @ViewChild("products") products: ElementRef;

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private renderer: Renderer2,
        private _fuseTranslationLoaderService: FuseTranslationLoaderService
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
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
}
