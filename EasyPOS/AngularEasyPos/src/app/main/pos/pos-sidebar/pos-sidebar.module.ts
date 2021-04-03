import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatRippleModule } from '@angular/material/core';

import { PosSidebarComponent } from './pos-sidebar.component';
import { PaymentMethodsComponent } from '../payment-methods/payment-methods.component';
import { TranslateModule } from '@ngx-translate/core';
import { MatMenuModule } from '@angular/material/menu';


@NgModule({
    declarations: [
        PaymentMethodsComponent,
        PosSidebarComponent
    ],
    imports     : [
        CommonModule,
        RouterModule,
        TranslateModule,

        MatDialogModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatIconModule,
        MatChipsModule,
        MatRippleModule,
        MatDividerModule,

    ],
    exports     : [
        PosSidebarComponent
    ]
})
export class PosSidebarModule
{
}
