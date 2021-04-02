import { Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { fuseAnimations } from '@fuse/animations';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';


@Component({
    selector   : 'pos-sidebar',
    templateUrl: './pos-sidebar.component.html',
    styleUrls  : ['./pos-sidebar.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class PosSidebarComponent
{
    centered = false;
    disabled = false;
    unbounded = false;

    radius: number;
    color: string;


    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.color = "rgba(223, 196, 0, 0.11)"
    }

}
