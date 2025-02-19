import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { OrderListComponent } from './order-list/order-list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { OrderService } from './order.service';
import { OrderDetailsComponent } from './order/order-details/order-details.component';
import { CreditNotesComponent } from './order/credit-notes/credit-notes.component';
import { OrderComponent } from './order/order.component';
import { OrderProductsComponent } from './order/order-products/order-products.component';
import { SearchModule } from 'app/layout/components/search/search.module';

const routes = [
    {
        path: 'document-list',
        component: OrderListComponent
    },
    {
        path: 'document',
        component: OrderComponent,
        resolve: {
            data: OrderService
        }
    },
    {
        path: 'document/:id',
        component: OrderComponent,
        resolve: {
            data: OrderService
        }
    },
    {
        path: 'document/:id/:handle',
        component: OrderComponent,
        resolve: {
            data: OrderService
        }
    },
];

@NgModule({
    declarations: [
        OrderListComponent,
        OrderComponent,
        OrderDetailsComponent,
        OrderProductsComponent,
        CreditNotesComponent
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
        OrderListComponent,
        OrderComponent,
        OrderDetailsComponent,
        OrderProductsComponent,
        CreditNotesComponent
    ],
    providers: [
        OrderService,
    ]
})

export class OrdersModule {
}
