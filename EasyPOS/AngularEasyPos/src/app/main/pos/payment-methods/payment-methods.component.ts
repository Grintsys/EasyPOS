import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
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

    centered = false;
    disabled = false;
    unbounded = false;

    radius: number;
    color: string;

    paymentMethod:Object[];

    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     * @param {MatDialogRef<MailNgrxComposeDialogComponent>} matDialogRef
     * @param _data
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        public matDialogRef: MatDialogRef<PaymentMethodsComponent>,
        @Inject(MAT_DIALOG_DATA) private _data: any,
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.paymentMethod = [
            {
                'title': 'Efectivo',
                'icon': 'cash',
                'isSelected': true
            },
            {
                'title': 'Transferencia',
                'icon': 'transfer',
                'isSelected': false
            },
            {
                'title': 'Tarjeta',
                'icon': 'credit-card',
                'isSelected': false
            },
            {
                'title': 'Cheque',
                'icon': 'check',
                'isSelected': false
            }
        ];
    }

}
