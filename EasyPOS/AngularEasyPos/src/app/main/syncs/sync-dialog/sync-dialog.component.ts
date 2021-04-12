import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';

@Component({
    selector   : 'sync-dialog',
    templateUrl: './sync-dialog.component.html',
    styleUrls  : ['./sync-dialog.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class SyncDialogComponent implements OnInit
{

    formatJsonForm: FormGroup;
    jsonFormat: string;

    centered = false;
    disabled = false;
    unbounded = false;

    radius: number;
    color: string;

    paymentMethod:Object[];

    /**
     * Constructor
     * @param {FormBuilder} _formBuilder
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     * @param {MatDialogRef<MailNgrxComposeDialogComponent>} matDialogRef
     * @param _data
     */
    constructor(
        private _formBuilder: FormBuilder,
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        public matDialogRef: MatDialogRef<SyncDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private _data: any,
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
    }

    /**
     * On init
     */
    ngOnInit(): void
    {
        this.formatJsonForm = this.createJsonForm();
    }


    /**
     * Create product form
     *
     * @returns {FormGroup}
    */
    createJsonForm(): FormGroup
    {
    return this._formBuilder.group({
            jsonFormat : new FormControl(this.jsonFormat),
        });
    }

}
