import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

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
import { MatToolbarModule } from '@angular/material/toolbar';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';


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
        MatToolbarModule,

        MatButtonModule,
        MatButtonToggleModule,
        MatIconModule,
        MatChipsModule,
        MatRippleModule,
        MatDividerModule,
        FlexLayoutModule,
        MatFormFieldModule,
        MatInputModule,

    ],
    exports     : [
        PosSidebarComponent
    ]
})
export class PosSidebarModule
{
}
