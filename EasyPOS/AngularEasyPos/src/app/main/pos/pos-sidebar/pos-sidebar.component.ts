import { Component, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { fuseAnimations } from '@fuse/animations';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { PaymentMethodsComponent } from '../payment-methods/payment-methods.component';


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

    dialogRef: any;


    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _matDialog: MatDialog,
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.color = "rgba(223, 196, 0, 0.11)"
    }

    openDialog(): void
    {
        this.dialogRef = this._matDialog.open(PaymentMethodsComponent, {
            panelClass: 'payment-method-dialog'
        });

        this.dialogRef.afterClosed()
            .subscribe(response => {
                if ( !response )
                {
                    return;
                }
                const actionType: string = response[0];
                switch ( actionType )
                {
                    /**
                     * Send
                     */
                    case 'send':
                        console.log('new Mail');
                        break;
                    /**
                     * Delete
                     */
                    case 'delete':
                        console.log('delete Mail');
                        break;
                }
            });
    }

}
