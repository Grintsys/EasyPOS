import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';


import { SearchBarModule } from '../pos/search-bar/search-bar.module';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';

import { MatTabsModule } from '@angular/material/tabs';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { MatChipsModule } from '@angular/material/chips';

import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerComponent } from './customer/customer.component';

import { CustomerService } from './customer/customer.service';

const routes = [
    {
        path     : 'customer-list',
        component: CustomerListComponent
    },
    {
        path     : 'customer',
        component: CustomerComponent,
        resolve  : {
            data: CustomerService
        }
    },
    {
        path     : 'customer/:id',
        component: CustomerComponent,
        resolve  : {
            data: CustomerService
        }
    },
    {
        path     : 'customer/:id/:handle',
        component: CustomerComponent,
        resolve  : {
            data: CustomerService
        }
    },
];

@NgModule({
    declarations: [
        CustomerListComponent,
        CustomerComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule,

        SearchBarModule,

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
    ],
    exports     : [
        CustomerListComponent,
        CustomerComponent
    ],
    providers : [
        CustomerService,
    ]
})

export class CustomersModule
{
}
