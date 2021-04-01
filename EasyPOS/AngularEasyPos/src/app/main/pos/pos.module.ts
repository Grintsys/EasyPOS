import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSearchBarModule } from 'app/layout/components/search-bar/search-bar.module';

import { PosComponent } from './pos.component';
import { CartProductsComponent } from './cart-products/cart-products.component';

const routes = [
    {
        path     : 'pos',
        component: PosComponent
    }
];

@NgModule({
    declarations: [
        PosComponent,
        CartProductsComponent,
    ],
    imports     : [
        RouterModule.forChild(routes),
        TranslateModule,
        FuseSharedModule,
        FuseSearchBarModule,
        MatPaginatorModule,
        MatSortModule,
        MatTableModule,
        MatIconModule,
        MatButtonModule,
    ],
    exports     : [
        PosComponent,
    ]
})

export class PosModule
{
}
