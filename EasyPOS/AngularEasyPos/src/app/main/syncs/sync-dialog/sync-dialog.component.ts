import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { SyncDto } from '../sync.model';

@Component({
    selector: 'sync-dialog',
    templateUrl: './sync-dialog.component.html',
    styleUrls: ['./sync-dialog.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class SyncDialogComponent implements OnInit {

    formatJsonForm: FormGroup;
    syncData: SyncDto;

    centered = false;
    disabled = false;
    unbounded = false;

    radius: number;
    color: string;

    paymentMethod: Object[];
    dialogType: string;

    constructor(
        private _formBuilder: FormBuilder,
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        public matDialogRef: MatDialogRef<SyncDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private _data: any,
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        this.syncData = _data.syncDto;
        this.dialogType = _data.mode;
    }

    ngOnInit(): void {
        this.formatJsonForm = this.createJsonForm();
    }

    save() {
        this.matDialogRef.close(this.syncData.data);
    }

    close(){
        this.matDialogRef.close(undefined);
    }

    dataChange(event) {
        this.syncData.data = event;
    }

    createJsonForm(): FormGroup {
        return this._formBuilder.group({
            jsonData: new FormControl({
                value: this.syncData.data, disabled: this.dialogType == 'view'
            }),
            message: new FormControl({
                value: this.syncData.message, disabled: true
            })
        });
    }

}
