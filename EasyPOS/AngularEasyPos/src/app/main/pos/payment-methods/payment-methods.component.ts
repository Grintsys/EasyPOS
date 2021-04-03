import { Component, ViewEncapsulation } from '@angular/core';


import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';

@Component({
    selector   : 'payment-methods',
    templateUrl: './payment-methods.component.html',
    styleUrls  : ['./payment-methods.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class PaymentMethodsComponent
{

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
    }

}
