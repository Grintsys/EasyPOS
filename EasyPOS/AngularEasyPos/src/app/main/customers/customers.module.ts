import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';


import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';

import { MatTabsModule } from '@angular/material/tabs';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { MatChipsModule } from '@angular/material/chips';

import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerComponent } from './customer/customer.component';

import { CustomerService } from './customer.service';
import { CustomerDialogComponent } from './customer-dialog/customer-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SearchModule } from 'app/layout/components/search/search.module';

const routes = [
    {
        path: 'customer-list',
        component: CustomerListComponent
    },
    {
        path: 'customer',
        component: CustomerComponent,
        resolve: {
            data: CustomerService
        }
    },
    {
        path: 'customer/:id',
        component: CustomerComponent,
        resolve: {
            data: CustomerService
        }
    },
    {
        path: 'customer/:id/:handle',
        component: CustomerComponent,
        resolve: {
            data: CustomerService
        }
    },
];

@NgModule({
    declarations: [
        CustomerListComponent,
        CustomerComponent,
        CustomerDialogComponent
    ],
    imports: [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule,

        SearchModule,

        MatTableModule,
        MatSortModule,
        MatPaginatorModule,
        MatMenuModule,

        MatFormFieldModule,
        MatInputModule,
        MatTabsModule,
        MatChipsModule,

        MatButtonModule,
        MatIconModule,

        MatDividerModule,

        MatDialogModule,
        MatToolbarModule,
    ],
    exports: [
        CustomerListComponent,
        CustomerComponent,
        CustomerDialogComponent
    ],
    providers: [
        CustomerService,
    ]
})

export class CustomersModule {
}
