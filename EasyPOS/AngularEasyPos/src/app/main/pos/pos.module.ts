import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { FuseSharedModule } from '@fuse/shared.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatMenuModule } from '@angular/material/menu';

// ELEMENT UI
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';

// MODULE UI
import { SearchBarModule } from './search-bar/search-bar.module';
import { PosSidebarModule } from './pos-sidebar/pos-sidebar.module';

// COMPONENT UI
import { PosComponent } from './pos.component';
import { PosProductsComponent } from './pos-products/pos-products.component';

const routes = [
    {
        path     : 'pos',
        component: PosComponent
    }
];

@NgModule({
    declarations: [
        PosComponent,
        PosProductsComponent
    ],
    imports     : [
        RouterModule.forChild(routes),
        FlexLayoutModule,
        TranslateModule,
        FuseSharedModule,
        MatMenuModule,

        SearchBarModule,
        PosSidebarModule,

        MatTableModule,
        MatSortModule,
        MatPaginatorModule,

        MatDialogModule,
        MatButtonModule,
        MatIconModule,
    ],
    exports     : [
        PosComponent,
    ]
})

export class PosModule
{
}
