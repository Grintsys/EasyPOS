import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';
import { ProductComponent } from './product/product.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductService } from './product.service';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SearchModule } from 'app/layout/components/search/search.module';


const routes = [
    {
        path     : 'product-list',
        component: ProductListComponent
    },
    {
        path     : 'product',
        component: ProductComponent,
        resolve  : {
            data: ProductService
        }
    },
    {
        path     : 'product/:id',
        component: ProductComponent,
        resolve  : {
            data: ProductService
        }
    },
    {
        path     : 'product/:id/:handle',
        component: ProductComponent,
        resolve  : {
            data: ProductService
        }
    },
];

@NgModule({
    declarations: [
        ProductComponent,
        ProductListComponent
    ],
    imports     : [
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
    exports     : [
        ProductComponent,
        ProductListComponent
    ],
    providers : [
        ProductService,
    ]
})

export class ProductsModule
{
}
