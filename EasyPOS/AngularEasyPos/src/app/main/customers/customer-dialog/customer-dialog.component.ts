import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';

@Component({
    selector: 'customer-dialog',
    templateUrl: './customer-dialog.component.html',
    styleUrls: ['./customer-dialog.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class CustomerDialogComponent {

    centered = false;
    disabled = false;
    unbounded = false;

    radius: number;
    color: string;

    paymentMethod: Object[];

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        public matDialogRef: MatDialogRef<CustomerDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private _data: any,
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
    }

}
