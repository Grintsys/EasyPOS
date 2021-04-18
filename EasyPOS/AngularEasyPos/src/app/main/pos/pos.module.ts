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
import { PosSidebarModule } from './pos-sidebar/pos-sidebar.module';

// COMPONENT UI
import { PosComponent } from './pos.component';
import { PosService } from './pos.service';
import { PosProductsComponent } from './pos-products/pos-products.component';
import { SearchModule } from 'app/layout/components/search/search.module';
import { SearchResultsModule } from './search-results/search-results.module';

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

        SearchModule,
        SearchResultsModule,
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
    ],
    providers : [
        PosService,
    ]
})

export class PosModule
{
}
